using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour
{
    public EnemyObject enemyObject;
    protected NavMeshAgent agent;
    [SerializeField] private Transform playerTransform;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(agent == null) agent = GetComponentInParent<NavMeshAgent>();
        agent.speed = enemyObject.Speed;
    }

    protected virtual void Update()
    {
        if (CanSeePlayer())
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
}
