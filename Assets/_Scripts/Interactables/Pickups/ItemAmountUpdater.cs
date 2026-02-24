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
        //tes
        if (amount == 0)
        {
            keyCounter.text = " ";
            keyCounter.gameObject.SetActive(false);
        }

        else
        {
            keyCounter.gameObject.SetActive(true);
            keyCounter.text = "Keys : " + amount.ToString();
        }
    }
    private void UpdatePotionAmount(int amount)
    {
        if (amount == 0)
        {
            potionCounter.text = " ";
            potionCounter.gameObject.SetActive(false);
        }

        else
        {
            potionCounter.gameObject.SetActive(true);
            potionCounter.text = "Potions : " + amount.ToString();
        }
    }
    private void UpdateCoinAmount(int amount)
    {
        if (amount == 0)
        {
            coinCounter.text = " ";
            coinCounter.gameObject.SetActive(false);
        }

        else
        {
            coinCounter.gameObject.SetActive(true);
            coinCounter.text = "Coins : " + amount.ToString();
        }
    }
    private void OnDisable()
    {
        PlayerInventory.OnKeyAmountChanged -= UpdateKeyAmount;
        PlayerInventory.OnPotionAmountChanged -= UpdatePotionAmount;
        PlayerInventory.OnCoinAmountChanged -= UpdateCoinAmount;
    }
}
