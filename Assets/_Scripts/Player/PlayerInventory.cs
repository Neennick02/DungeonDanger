using System;
using System.Collections;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    TargetFinder targetFinder;
    PlayerAnimator animator;
    public int keyAmount {get; private set;}
    public int potionAmount { get; private set;}

    public int coinAmount { get; private set; }

    public static event Action<int> OnKeyAmountChanged;
    public static event Action<int> OnPotionAmountChanged;
    public static event Action<int> OnCoinAmountChanged;

    public bool swordInHand { get; private set; }
    private bool shieldInHand = false;

    [SerializeField] private GameObject sword, shield;
    


    private void Start()
    {
        targetFinder = GetComponent<TargetFinder>();
        animator = GetComponentInChildren<PlayerAnimator>();    

        OnKeyAmountChanged?.Invoke(keyAmount);
        OnPotionAmountChanged?.Invoke(potionAmount);
        OnCoinAmountChanged?.Invoke(coinAmount);

        PlayerAttack.OnGrabSword += GrabSword;
        PlayerInput.OnPutAway += PutAway;
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

    public void GrabSword()
    {
        if(!swordInHand){
             swordInHand = true;
            StartCoroutine(ActivateSword(true));
        }
    }
    public void PutAway()
    {
        if (targetFinder.currentTarget != null) return;

        if (swordInHand)
        {
            swordInHand = false;
            animator.PutAway();
            StartCoroutine(ActivateSword(false));
        }
    }

    IEnumerator ActivateSword(bool isActive)
    {
        yield return new WaitForSeconds(0.4f);
        sword.SetActive(isActive);
    }
}
