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

    public override void DrainHealth(int amount)
    {
        currentHealth -= amount;
        FlashRed();

        if (currentHealth <= 0 && !isDead)
        {

            TargetFinder.RemoveFromPool(targetScript.transform);
            Die();
            isDead = true;
        }
    }
}
