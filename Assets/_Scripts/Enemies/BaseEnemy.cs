using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour
{
    public EnemyObject enemyObject;
    protected NavMeshAgent agent;
    protected Transform playerTransform;

    protected bool isAttacking;
    protected float timer = 0;

    protected bool isDead = false;
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(agent == null) agent = GetComponentInParent<NavMeshAgent>();

        agent.speed = enemyObject.Speed;
        agent.stoppingDistance = enemyObject.AttackDistance;
    }

    protected virtual void Update()
    {
        if (CanSeePlayer() && !isDead)
        {
            agent.destination = playerTransform.position;
            transform.LookAt(playerTransform.position);
        }
    }

    protected bool CanSeePlayer()
    {
        if (playerTransform == null) return false;

        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if(distance < enemyObject.SpottingRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual void Attack()
    {
        //add code to sub classes
    }

    public virtual void Die()
    {
        //add code to sub classes
    }

    public void AssignPlayer(Transform transform)
    {
        playerTransform = transform;
    }
}
