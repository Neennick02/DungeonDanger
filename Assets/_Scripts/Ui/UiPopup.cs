using System.Collections;
using TMPro;
using UnityEngine;

public class UiPopup : MonoBehaviour
{
    [SerializeField] private GameObject panelObject;
    [SerializeField] private TextMeshProUGUI textObject;

    private void OnEnable()
    {
        SaveStatueInteractable.OnSavePlayerData += ShowSavePopup;
    }

    private void OnDisable()
    {
        SaveStatueInteractable.OnSavePlayerData += ShowSavePopup;
    }

    public void ShowSavePopup()
    {
        StartCoroutine(PopupRoutine("Game Saved"));
    }

    IEnumerator PopupRoutine(string popupText)
    {
        textObject.text = popupText;
        panelObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        panelObject.SetActive(false);
    }
}
