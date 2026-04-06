using System.Collections.Generic;
using UnityEngine;

public class WarriorHealth : BaseHealth
{
    private WarriorAnimator animator;
    private WarriorEnemy enemyScript;
    [SerializeField] private List<AudioClip> boneSounds;
    [SerializeField] private List<AudioClip> deathSounds;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<WarriorAnimator>();
        enemyScript = GetComponent<WarriorEnemy>();

        targetTransform = GetComponentInChildren<Target>().transform;
    }
    public override void DrainHealth(int amount)
    {
        AudioManager.Instance.PlayClip(boneSounds);

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
        AudioManager.Instance.PlayClip(deathSounds);

        base.Die();
        animator.EnableTrigger("Die");
        enemyScript.Die();
        StartCoroutine(DestroyAfterSeconds(2f));
    }
}
