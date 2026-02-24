using UnityEngine;

public abstract class BaseHealth : MonoBehaviour
{
    public static int MaxHealth = 20;
    protected int currentHealth;

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
        //add code to sub classes
    }

}
