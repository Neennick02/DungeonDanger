using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PokeApi : MonoBehaviour
{
    public TMP_InputField inputField;
    public Image sprite;

    private void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if(inputField.text.Length > 0)
            {
                StartCoroutine(CheckApi());
            }
        }
    }

    private IEnumerator CheckApi()
    {
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

            PokeData data;
            data = JsonUtility.FromJson<PokeData>(request.downloadHandler.text);
            Debug.Log(data.name);
        }
    }
    public class PokeData
    {
        public string name;
        public Image sprite;

    }
}