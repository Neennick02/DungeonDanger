using System;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBarManager : MonoBehaviour
{
    [SerializeField] private Image shieldBar;
    private bool isDefending;
    private float timer;

    private float shieldDuration;

    private float rechargeTimer;
    private float rechargeDuration;
    private void OnEnable()
    {
        PlayerInputHandler.OnDefendStart += StartShield;
        PlayerInputHandler.OnDefendEnd += EndShield;


        shieldDuration = PlayerInventory.maxShieldDuration;
    }

    private void OnDisable()
    {
        PlayerInputHandler.OnDefendStart -= StartShield;

    }
    private void Update()
    {
        if (isDefending)
        {
            timer += Time.deltaTime;

            shieldBar.fillAmount = 1 - (timer / shieldDuration);
        }

        if(!isDefending)
        {
            rechargeTimer += Time.deltaTime;
            shieldBar.fillAmount = rechargeTimer / rechargeDuration;
        }
    }

    private void StartShield()
    {
       isDefending = true;
        timer = 0;
    }

    private void EndShield()
    {
        isDefending = false;
        rechargeTimer = 0;
    }
}
