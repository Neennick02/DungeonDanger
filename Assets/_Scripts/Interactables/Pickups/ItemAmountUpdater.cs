using System;
using TMPro;
using UnityEngine;

public class ItemAmountUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyCounter;
    [SerializeField] private TextMeshProUGUI potionCounter;
    private void OnEnable()
    {
        PlayerInventory.OnKeyAmountChanged += UpdateKeyAmount;
        PlayerInventory.OnPotionAmountChanged += UpdatePotionAmount;
    }

    private void UpdateKeyAmount(int amount)
    {
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
    private void OnDisable()
    {
        PlayerInventory.OnKeyAmountChanged -= UpdateKeyAmount;
        PlayerInventory.OnPotionAmountChanged -= UpdatePotionAmount;
    }
}
