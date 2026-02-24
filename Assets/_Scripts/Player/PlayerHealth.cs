using System;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    public static event Action<int> OnHealthAmountChanged;
    protected override void Start()
    {
        base.Start();
        OnHealthAmountChanged?.Invoke(currentHealth);
    }
    public override void AddHealth(int amount)
    {
        currentHealth += amount;
        OnHealthAmountChanged?.Invoke(currentHealth);
    }

    public override void DrainHealth(int amount)
    {
        currentHealth -= amount;
        OnHealthAmountChanged?.Invoke(currentHealth);
    }

}
