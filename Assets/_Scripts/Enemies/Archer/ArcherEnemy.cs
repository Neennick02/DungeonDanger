using System.Collections;
using UnityEngine;

public class ArcherEnemy : BaseEnemy
{
    [SerializeField] private float arrowForce = 10f;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;
    WarriorAnimator animator;
    private float targetDistanceToPlayer = 5f;

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

        //walk back when to close
        if (distanceToPlayer < targetDistanceToPlayer)
        {
            agent.stoppingDistance = 1f;
            Vector3 targetDestination = playerTransform.position - transform.position.normalized * -10f;
            agent.destination = targetDestination;
        }
        else
        {
            agent.stoppingDistance = enemyObject.AttackDistance;
        }

        if (distanceToPlayer < enemyObject.AttackDistance)
        {
            //if in correct range ==> attack
            if (!isAttacking && timer >= enemyObject.AttackInterval)
            {
                animator.EnableTrigger("Attack");
                StartCoroutine(AttackRoutine());
                isAttacking = true;
                timer = 0;
            }
        }
    }

    IEnumerator AttackRoutine()
    {
        //wait for draw arrow animation
        yield return new WaitForSeconds(0.5f);

        GameObject arrowGo = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = arrowGo.GetComponent<Rigidbody>();

        //calculate slight offset - prevents perfect aim
        Vector3 targetPosition = playerTransform.position + new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f));

        Vector3 direction =  targetPosition - transform.position;
        rb.AddForce(direction * arrowForce, ForceMode.Impulse);

        isAttacking = false;
    }

    public override void Die()
    {
        transform.LookAt(null);
        agent.speed = 0f;
        agent.destination = transform.position;
        agent.angularSpeed = 0f;
    }
}
