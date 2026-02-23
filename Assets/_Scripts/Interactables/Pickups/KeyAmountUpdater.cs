using System;
using TMPro;
using UnityEngine;

public class KeyAmountUpdater : MonoBehaviour
{
    TextMeshProUGUI text;
    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        PlayerInventory.OnKeyAmountChanged += UpdateKeyAmount;
        
    }

    private void UpdateKeyAmount(int amount)
    {
        text.text = "Keys : " + amount.ToString();
    }

    private void OnDisable()
    {
        PlayerInventory.OnKeyAmountChanged -= UpdateKeyAmount;

    }
    private void UpdateKeyAmount()
    {
        PlayerInventory.OnKeyAmountChanged -= UpdateKeyAmount;
    }
}
