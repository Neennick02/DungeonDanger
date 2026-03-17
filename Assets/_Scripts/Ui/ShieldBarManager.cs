using System;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBarManager : MonoBehaviour
{
    [SerializeField] private Image shieldBar;


    private void OnEnable()
    {
        PlayerInventory.UpdateBar += UpdateBar;
    }

    private void OnDisable()
    {
        PlayerInventory.UpdateBar -= UpdateBar;
    }

    private void UpdateBar(float amount)
    {
        shieldBar.fillAmount = amount;
    }
}
