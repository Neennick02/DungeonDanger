

using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float offSet = 1.5f;
    [SerializeField] private Transform parent;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = parent.position;
        float distance = Vector3.Distance(transform.position, targetPos);

        // Only move if too far
        if (distance > offSet)
        {
            // Move at constant speed
            Vector3 direction = (targetPos - transform.position).normalized;
            Vector3 newPos = transform.position + direction * speed * Time.fixedDeltaTime;

            rb.MovePosition(newPos);
        }

        // Smooth rotation instead of instant LookAt
        Vector3 dir = (parent.position - transform.position);
        if (dir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, 10f * Time.fixedDeltaTime));
        }
    }
}
