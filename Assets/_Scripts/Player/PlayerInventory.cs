using System;
using System.Collections;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int keyAmount {get; private set;}
    public int potionAmount { get; private set;}
    public int coinAmount { get; private set; }

    public static event Action<int> OnKeyAmountChanged;
    public static event Action<int> OnPotionAmountChanged;
    public static event Action<int> OnCoinAmountChanged;


    [SerializeField] private GameObject sword, shield;

    #region OnEnable
    private void OnEnable()
    {
        KeyPickup.OnPickup += UpdateKeyAmount;
        PotionPickup.OnPickup += UpdatePotionAmount;
        CoinPickup.OnPickup += UpdateCoinAmount;

    }

    private void OnDisable()
    {
        KeyPickup.OnPickup -= UpdateKeyAmount;
        PotionPickup.OnPickup -= UpdatePotionAmount;
        CoinPickup.OnPickup -= UpdateCoinAmount;
    }

    #endregion

    private void Start()
    {
        OnKeyAmountChanged?.Invoke(keyAmount);
        OnPotionAmountChanged?.Invoke(potionAmount);
        OnCoinAmountChanged?.Invoke(coinAmount);
    }

    //keys

    public void UpdateKeyAmount(int amount)
    {
        keyAmount += amount;
        OnKeyAmountChanged?.Invoke(keyAmount);
    }

    //potions
    public void UpdatePotionAmount(int amount)
    {
        potionAmount += amount;
        OnPotionAmountChanged?.Invoke(potionAmount);
    }

    //coins
    public void UpdateCoinAmount(int amount)
    {
        coinAmount += amount;
        OnCoinAmountChanged?.Invoke(coinAmount);
    }
}
