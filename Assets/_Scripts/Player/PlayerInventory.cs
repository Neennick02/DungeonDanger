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


    [SerializeField] private GameObject shield;


    private bool isDefending;
    private bool shieldEnded = true;

    private float chargeAmount = 1;
    [SerializeField]  private float shieldChargeRate = 0.5f;

    public static event Action OnStartDefend;
    public static event Action OnFinishDefend;

    public static event Action<float> UpdateBar;
    public static event Action<bool> OnDefendActive;
    #region OnEnable
    private void OnEnable()
    {
        KeyPickup.OnPickup += UpdateKeyAmount;
        PotionPickup.OnPickup += UpdatePotionAmount;
        CoinPickup.OnPickup += UpdateCoinAmount;

        SaveStatueInteractable.OnSavePlayerData += SaveData;
        GameManager.OnLoad += LoadData;

        PlayerInputHandler.OnDefendStart += GrabShield;
        PlayerInputHandler.OnDefendEnd += PutAwayShield;
    }

    private void OnDisable()
    {
        KeyPickup.OnPickup -= UpdateKeyAmount;
        PotionPickup.OnPickup -= UpdatePotionAmount;
        CoinPickup.OnPickup -= UpdateCoinAmount;

        SaveStatueInteractable.OnSavePlayerData -= SaveData;
        GameManager.OnLoad -= LoadData;

        PlayerInputHandler.OnDefendStart -= GrabShield;
        PlayerInputHandler.OnDefendEnd -= PutAwayShield;
    }

    #endregion

    private void Start()
    {
        OnKeyAmountChanged?.Invoke(keyAmount);
        OnPotionAmountChanged?.Invoke(potionAmount);
        OnCoinAmountChanged?.Invoke(coinAmount);
    }

    private void Update()
    {
        chargeAmount = Mathf.Clamp(chargeAmount, 0, 1);

        //discharge shield
        if (chargeAmount > 0 && isDefending)
        {
            chargeAmount -= Time.deltaTime * shieldChargeRate;

        }
        //if charge is depleted
        else
        {
            chargeAmount += Time.deltaTime * shieldChargeRate;

            if (!shieldEnded)
            {
                OnFinishDefend?.Invoke();
                shieldEnded = true;
            }
        }
        //updates bar fillamount
        UpdateBar?.Invoke(chargeAmount);
        
        //updates health script
        OnDefendActive?.Invoke(isDefending);
    }

    private void GrabShield()
    {
        if (chargeAmount > 0)
        {
            isDefending = true;
            shieldEnded = false;
            OnStartDefend?.Invoke();
        }
    }

    private void PutAwayShield()
    {
        isDefending = false;
        OnFinishDefend?.Invoke();
    }
    public void UpdateKeyAmount(int amount)
    {
        keyAmount += amount;
        if (keyAmount < 0) keyAmount = 0;
        OnKeyAmountChanged?.Invoke(keyAmount);
    }

    //potions
    public void UpdatePotionAmount(int amount)
    {
        potionAmount += amount;
        if (potionAmount < 0) potionAmount = 0;
        OnPotionAmountChanged?.Invoke(potionAmount);
    }

    //coins
    public void UpdateCoinAmount(int amount)
    {
        coinAmount += amount;
        if (coinAmount < 0) coinAmount = 0;
        OnCoinAmountChanged?.Invoke(coinAmount);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("coinAmount", coinAmount);
        PlayerPrefs.SetInt("keyAmount", keyAmount);
        PlayerPrefs.SetInt("potionAmount", potionAmount);
    }
    private void LoadData()
    {
        coinAmount = PlayerPrefs.GetInt("coinAmount", coinAmount);
        keyAmount = PlayerPrefs.GetInt("keyAmount", keyAmount);
        potionAmount = PlayerPrefs.GetInt("potionAmount", potionAmount);
    }
}
