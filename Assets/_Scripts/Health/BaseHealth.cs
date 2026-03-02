using System;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour
{
    public static int MaxHealth = 20;
    protected int currentHealth;
    protected bool isDead = false;
    public static event Action SwitchTarget;

    protected virtual void Start()
    {
        currentHealth = MaxHealth;
    }

    protected virtual void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
        CheckHealth();
    }

    public virtual void DrainHealth(int amount)
    {
        currentHealth -= amount;
    }
    public virtual void AddHealth(int amount)
    {
        currentHealth += amount;
    }

    protected virtual void CheckHealth()
    {
        if(currentHealth <= 0 && !isDead)
        {
            Die();
            isDead = true;
        }
    }
    protected virtual void Die()
    {
        //add code to sub classes
        if(!this.transform.CompareTag("Player")){
            SwitchTarget?.Invoke();
            TargetFinder.RemoveFromPool(transform);
            Destroy(gameObject);
        }
    }

}
