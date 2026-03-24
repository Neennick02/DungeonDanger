using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBehaviour : MonoBehaviour
{
    public Transform head;
    public List<Transform> segments = new List<Transform>();
    public float segmentSpacing = 0.5f;
    public float smoothSpeed = 10f;

    private List<Vector3> positions = new List<Vector3>();

    void Start()
    {
        positions.Add(head.position);
    }

    void Update()
    {
        // Store head position
        if (Vector3.Distance(positions[0], head.position) > segmentSpacing)
        {
            positions.Insert(0, head.position);
        }

        // Move segments
        for (int i = 0; i < segments.Count; i++)
        {
            Vector3 targetPos = positions[Mathf.Min(i + 1, positions.Count - 1)];

            segments[i].position = Vector3.Lerp(
                segments[i].position,
                targetPos,
                smoothSpeed * Time.deltaTime
            );

            // Optional: rotate toward movement direction
            Vector3 dir = targetPos - segments[i].position;
            if (dir != Vector3.zero)
                segments[i].rotation = Quaternion.LookRotation(dir);
        }

        // Trim stored positions
        if (positions.Count > segments.Count + 1)
            positions.RemoveAt(positions.Count - 1);
    }
}
