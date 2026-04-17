using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PokeApi : MonoBehaviour
{
    public TMP_InputField inputField;
    public Image Preview;
    public AudioSource Source;
    private void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            
                StartCoroutine(CheckApi());
            
        }
    }

    private IEnumerator CheckApi()
    {
        PokeData data;

        //zoekt de api op
        using (UnityWebRequest request = UnityWebRequest.Get("https://pokeapi.co/api/v2/pokemon/" + inputField.text))
        {
            //wacht op api server / verbinding
            yield return request.SendWebRequest();
            
            //check resultaat van de zoek
            if(request.result != UnityWebRequest.Result.Success)
            {
                yield break;
            }

            data = JsonUtility.FromJson<PokeData>(request.downloadHandler.text);
            Debug.Log(data.id);
        }

        if(data != null)
        {
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(data.sprites.front_default))
            {
                yield return request.SendWebRequest();
                if (request.result != UnityWebRequest.Result.Success)
                {
                    yield break;
                }
                Texture2D preview = DownloadHandlerTexture.GetContent(request);
                Preview.sprite = Sprite.Create(preview, new Rect(0, 0, preview.width, preview.height), new Vector2(0.5f, 0.5f));
            }


            //audio
            using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(data.cries.legacy, AudioType.OGGVORBIS))
            {
                yield return request.SendWebRequest();
                if(request.result != UnityWebRequest.Result.Success)
                {
                    yield break;
                }
                AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
                Source.clip = clip;
                Source.Play();
            }
        }
    }

    [Serializable]
    public class PokeData
    {
        public string id;
        public PokeSprite sprites;
        public PokeCry cries;
    }
    [Serializable]
    public class PokeSprite
    {
        public string front_default;
        public string back_default;
        public string front_shiny;
        public string back_shiny;
    }
    [Serializable]
    public class PokeCry
    {
        public string legacy;
    }
}