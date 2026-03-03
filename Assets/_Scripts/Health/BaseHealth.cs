using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour
{
    public static int MaxHealth = 20;
    protected int currentHealth;
    protected bool isDead = false;
    public static event Action SwitchTarget;

    public Transform targetTransform;
    public MeshRenderer MeshRenderer;
    public Color DamageColor;
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
            TargetFinder.RemoveFromPool(targetTransform);
            SwitchTarget?.Invoke();
            Destroy(gameObject);
        }
    }

    public void FlashRed()
    {
        StopAllCoroutines();
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        Color standard = MeshRenderer.material.color;
        MeshRenderer.material.color = DamageColor;
        yield return new WaitForSeconds(0.1f);
        MeshRenderer.material.color = standard;
    }

}
