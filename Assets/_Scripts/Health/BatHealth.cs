using System;
using UnityEngine;
using UnityEngine.Events;

public class BatHealth : BaseHealth
{
    private Target targetScript;
    protected override void Start()
    {
        base.Start();
        targetScript = GetComponentInChildren<Target>();
    }
    protected override void Update()
    {
        base.Update();
    }
    public override void AddHealth(int amount)
    {
        currentHealth += amount;
    }

    public override void DrainHealth(int amount)
    {
        currentHealth -= amount;
        FlashRed();
    }
    protected override void Die()
    {
        //add code to sub classes
        if (!this.transform.CompareTag("Player"))
        {
            TargetFinder.RemoveFromPool(targetScript.transform);
            SwitchTarget?.Invoke();
            Destroy(gameObject);
        }
    }
}
