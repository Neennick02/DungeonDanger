using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour
{
    [SerializeField] private float characterDelay = 0.05f;
    [SerializeField] private float alineaDelay = 1f;
    [SerializeField] private float fadeOutDuration = 1f;
    private float fadeOutTimer;
    [SerializeField] private List<string> textList;
    [SerializeField] private Image blackScreen;

    private TextMeshProUGUI textObject;
    private void Start()
    {
        textObject = GetComponent<TextMeshProUGUI>();
        
        StartCoroutine(TypeRoutine());
    }

    private void Update()
    {
        if (Input.anyKey || (Gamepad.current != null && Gamepad.current.allControls.Any(c => c.IsPressed())))
        {
            characterDelay = 0.01f;
        }
        else characterDelay = 0.05f;
    }

    IEnumerator TypeRoutine()
    {
        yield return new WaitForSeconds(1f);

        for(int i = 0; i < textList.Count; i++)
        {
            textObject.text = textList[i];
            textObject.maxVisibleCharacters = 0;


            while (textObject.maxVisibleCharacters < textObject.text.Length)
            {
                textObject.maxVisibleCharacters++;
                yield return new WaitForSeconds(characterDelay);
            }

            yield return new WaitForSeconds(alineaDelay);
        }
        //fade out
        while(fadeOutTimer < fadeOutDuration)
        {
            fadeOutTimer += Time.deltaTime;
            blackScreen.color = Color.Lerp(Color.clear, Color.black, fadeOutTimer / fadeOutDuration);
            yield return null;
        }
        
        SceneManager.LoadScene("LevelBuildingScene");
    }
}
