using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int keyAmount {get; private set;}
    public int potionAmount { get; private set;}

    public static event Action<int> OnKeyAmountChanged;
    public static event Action<int> OnPotionAmountChanged;

    private void Start()
    {
        OnKeyAmountChanged?.Invoke(keyAmount);
        OnPotionAmountChanged?.Invoke(potionAmount);
    }


    public void AddKey(int amount)
    {
        keyAmount += amount;
        OnKeyAmountChanged?.Invoke(keyAmount);
    }

    public void RemoveKey(int amount)
    {
        keyAmount -= amount;
        OnKeyAmountChanged?.Invoke(keyAmount);
    }


    public void AddPotion(int amount)
    {
        potionAmount += amount;
        OnPotionAmountChanged?.Invoke(potionAmount);
    }

    public void RemovePotion(int amount)
    {
        potionAmount -= amount;
        OnPotionAmountChanged?.Invoke(potionAmount);
    }
}
