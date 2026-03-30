using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] private BossObject bossObject;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float spacing = 0.5f;
    [SerializeField] private List<Segment> Segments = new List<Segment>();
    
    public SplineContainer container;

    private Rigidbody rb;
    private float progress;
    private float startHeight;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        //save default height
        startHeight = rb.position.y;
    }

    private void FixedUpdate()
    {
        progress += speed * Time.fixedDeltaTime;

        if (progress >= 1)
        {
            progress = 0;
        }
        //update head position
        Vector3 position = container.EvaluatePosition(progress);
        Vector3 newPos = new Vector3(position.x, startHeight, position.z);
        rb.MovePosition(newPos);

        //update segments positions
        for (int i= 0; i < Segments.Count; i++)
        {
            //calculate position
            float childProgress = progress - spacing * (i + 1);
            childProgress = (childProgress + 1f) % 1f;

            Vector3 pos = container.EvaluatePosition(childProgress);
            Vector3 forward = container.EvaluateTangent(childProgress);
             
            Segments[i].UpdatePosition(pos, pos + forward);
        }


        Vector3 targetPos = container.EvaluatePosition(progress + 0.01f);
        //remove y value
        Vector3 finalRotationPos = new Vector3(targetPos.x, startHeight, targetPos.z);
        Vector3 rotationDirection = finalRotationPos - rb.position;
        rb.rotation = Quaternion.LookRotation(rotationDirection);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            health.DrainHealth(bossObject.Damage);
        }
    }
}
