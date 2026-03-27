using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    public SplineContainer container;
    private Rigidbody rb;
    private float progress;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        progress += speed * Time.deltaTime;
        if (progress > 1) progress = 0;

        Vector3 position = container.EvaluatePosition(progress);
        rb.MovePosition(position);


        Vector3 targetPos = container.EvaluatePosition(progress + 0.1f);
        Vector3 rotationDirection = targetPos - rb.position;
        rb.rotation = Quaternion.LookRotation(rotationDirection);
    }
}
