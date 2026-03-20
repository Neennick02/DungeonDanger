using System;
using TMPro;
using UnityEngine;

public class ItemAmountUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyCounter;
    [SerializeField] private TextMeshProUGUI potionCounter;
    [SerializeField] private TextMeshProUGUI coinCounter;

    private void OnEnable()
    {
        PlayerInventory.OnKeyAmountChanged += UpdateKeyAmount;
        PlayerInventory.OnPotionAmountChanged += UpdatePotionAmount;
        PlayerInventory.OnCoinAmountChanged += UpdateCoinAmount;
    }

    private void UpdateKeyAmount(int amount)
    {

            keyCounter.text = amount.ToString();
        
    }
    private void UpdatePotionAmount(int amount)
    {
            potionCounter.text = amount.ToString();
    }
    private void UpdateCoinAmount(int amount)
    {
            coinCounter.text = amount.ToString();
        
    }
    private void OnDisable()
    {
        PlayerInventory.OnKeyAmountChanged -= UpdateKeyAmount;
        PlayerInventory.OnPotionAmountChanged -= UpdatePotionAmount;
        PlayerInventory.OnCoinAmountChanged -= UpdateCoinAmount;
    }
}
