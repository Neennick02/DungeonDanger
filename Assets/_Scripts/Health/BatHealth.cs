using System;
using UnityEngine;
using UnityEngine.Events;

public class BatHealth : BaseHealth
{
    protected override void Start()
    {
        base.Start();
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
        base.Die();
    }
}
