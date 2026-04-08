using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEnemy : BaseEnemy
{
    WarriorAnimator animator;
    [SerializeField] private Collider swordCollider;
    [SerializeField] private List<AudioClip> swordAudioClips;
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<WarriorAnimator>();
    }
    protected override void Update()
    {
        if (isDead) return;

        base.Update();
        timer += Time.deltaTime;

        animator.UpdateSpeed(agent.velocity.magnitude);


        if (playerTransform == null) return;
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        if (distanceToPlayer < enemyObject.AttackDistance)
        {
            if (!isAttacking && timer > enemyObject.AttackInterval)
            {
                AudioManager.Instance.PlayClip(swordAudioClips);
                animator.EnableTrigger("Attack");
                Attack();
                isAttacking = true;
                timer  = 0; 
            }
        }
    }
    protected override void Attack()
    {
        StartCoroutine(AttackRoutine(1.5f));
    }

    public override void Die()
    {
        isDead = true;
        transform.LookAt(null);
        agent.speed = 0f;
        agent.destination = transform.position;
        agent.angularSpeed = 0f;
    }

    IEnumerator AttackRoutine(float time)
    {
        //wait before turning on sword
        yield return new WaitForSeconds(time/4);
        swordCollider.enabled = true;

        //wait for swing to end
        yield return new WaitForSeconds(time/4 * 3);
        //disable sword hitbox
        swordCollider.enabled = false;
        isAttacking = false;    
    }
}
