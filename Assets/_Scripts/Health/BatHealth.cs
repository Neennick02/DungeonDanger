using System;
using UnityEngine;

public class BatHealth : BaseHealth
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        Debug.Log(currentHealth);
    }
    public override void AddHealth(int amount)
    {
        currentHealth += amount;
    }

    public override void DrainHealth(int amount)
    {
        currentHealth -= amount;
    }
    protected override void Die()
    {
        base.Die();
    }
}
