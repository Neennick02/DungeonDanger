using UnityEngine;

public class WarriorHealth : BaseHealth
{
    private WarriorAnimator animator;
    private WarriorEnemy enemyScript;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<WarriorAnimator>();
        enemyScript = GetComponent<WarriorEnemy>();

        targetTransform = GetComponentInChildren<Target>().transform;
    }
    public override void DrainHealth(int amount)
    {
        Debug.Log("WAAA");
        currentHealth -= amount;
        FlashRed();
        if(currentHealth > 0)
        {
            animator.EnableTrigger("Damage");
        }
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    protected override void Die()
    {
        base.Die();
        animator.EnableTrigger("Die");
        enemyScript.Die();
        StartCoroutine(DestroyAfterSeconds(2f));
    }
}
