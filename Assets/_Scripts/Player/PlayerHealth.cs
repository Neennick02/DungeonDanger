using System;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    [SerializeField] private PlayerObject playerObject;
    public static event Action<int> OnHealthAmountChanged;
    public static event Action OnDeath;
    protected override void Start()
    {
        maxHealth = playerObject.StartHealth;
        currentHealth = maxHealth;
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
        OnDeath?.Invoke();
    }
}
