using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BatHealth : BaseHealth
{
    private Target targetScript;
    private BatEnemy enemyScript;
    protected override void Start()
    {
        base.Start();
        targetScript = GetComponentInChildren<Target>();
        enemyScript = GetComponentInChildren<BatEnemy>();
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

    protected override void Die()
    {
        base.Die();
        //removes navmesh component
        Destroy(gameObject.GetComponent<NavMeshAgent>());
        enemyScript.Die();
        

        StartCoroutine(DestroyAfterSeconds(2f));
    }
}
