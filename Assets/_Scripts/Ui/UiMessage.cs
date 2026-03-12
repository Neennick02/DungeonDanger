using TMPro;
using UnityEngine;

public class UiMessage : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    [SerializeField] TextMeshProUGUI text;

    private void OnEnable()
    {
        SignInteractable.OpenSign += OpenSign;
        SignInteractable.CloseSign += CloseSign;
    }
    private void OnDisable()
    {
        SignInteractable.OpenSign -= OpenSign;
        SignInteractable.CloseSign -= CloseSign;
    }
    
    private void OpenSign(string message)
    {
        if (text == null) Debug.Log("empt");

        holder.SetActive(true);
        text.text = message;
    }

    private void CloseSign()
    {
        holder.SetActive(false);
    }


}
