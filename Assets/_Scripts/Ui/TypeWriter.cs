using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TypeWriter : MonoBehaviour
{
    [SerializeField] private float delay = 0.05f;

    [SerializeField] private string StartText, MiddleText, EndText;

    private TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        

        text.text = StartText;
        text.maxVisibleCharacters = 0;
        StartCoroutine(TypeRoutine());
    }

    IEnumerator TypeRoutine()
    {
        yield return new WaitForSeconds(1f);

        while (text.maxVisibleCharacters < text.text.Length)
        {
            text.maxVisibleCharacters++;
            yield return new WaitForSeconds(delay);

        }
        yield return new WaitForSeconds(1f);

        text.text = MiddleText;
        text.maxVisibleCharacters = 0;
        while (text.maxVisibleCharacters < text.text.Length)
        {
            text.maxVisibleCharacters++;
            yield return new WaitForSeconds(delay);

        }
        yield return new WaitForSeconds(1f);
        text.text = EndText;
        text.maxVisibleCharacters = 0;
        while (text.maxVisibleCharacters < text.text.Length)
        {
            text.maxVisibleCharacters++;
            yield return new WaitForSeconds(delay);

        }
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("LevelBuildingScene");

    }
}
