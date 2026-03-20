using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBehaviour : MonoBehaviour
{

    [SerializeField] private List<GameObject> segments = new List<GameObject>();
    private NavMeshAgent agent;

    private void Start()
    {
       // agent = GetComponent<NavMeshAgent>();
    }

    private void OnCollisionEnter(Collision collision)
    {
       /* if (collision.gameObject.CompareTag("Terrain"))
        {
            agent.destination = Vector3.Reflect(transform.position, transform.right);
        }*/
    }
}
