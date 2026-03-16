using System.Collections;
using UnityEngine;

public class WarriorEnemy : BaseEnemy
{
    WarriorAnimator animator;
    [SerializeField] private Collider swordCollider;
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<WarriorAnimator>();
    }
    protected override void Update()
    {
        base.Update();
        timer += Time.deltaTime;

        animator.UpdateSpeed(agent.velocity.magnitude);


        if (playerTransform == null) return;
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        if (distanceToPlayer < enemyObject.AttackDistance)
        {
            if (!isAttacking && timer > enemyObject.AttackInterval)
            {
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
        agent.speed = 0f;
        agent.destination = transform.position;
        agent.angularSpeed = 0f;
    }

    IEnumerator AttackRoutine(float time)
    {
        swordCollider.enabled = true;
        yield return new WaitForSeconds(time);

        swordCollider.enabled = false;
        isAttacking = false;    
    }
}
