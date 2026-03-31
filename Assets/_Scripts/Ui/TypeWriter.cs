using System.Collections;
using TMPro;
using UnityEngine;

public class TypeWriter : MonoBehaviour
{
    [SerializeField] private float delay = 0.05f;
    private TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.maxVisibleCharacters = 0;
        StartCoroutine(TypeRoutine());
    }

    IEnumerator TypeRoutine()
    {
        while(text.maxVisibleCharacters < text.text.Length)
        {
            text.maxVisibleCharacters++;
            yield return new WaitForSeconds(delay);

        }
        yield return new WaitForSeconds(1f);
        Debug.Log("Done");

    }
}
