using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int keyAmount {get; private set;}
    public static event Action<int> OnKeyAmountChanged;

    private void Start()
    {
        OnKeyAmountChanged?.Invoke(keyAmount);
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
}
