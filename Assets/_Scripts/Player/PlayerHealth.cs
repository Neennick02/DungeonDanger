using System;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    [SerializeField] private PlayerObject playerObject;
    public static event Action<int> OnHealthAmountChanged;
    protected override void Start()
    {
        base.Start();
        maxHealth = playerObject.StartHealth;
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
    protected override void Die()
    {
        
    }
}
