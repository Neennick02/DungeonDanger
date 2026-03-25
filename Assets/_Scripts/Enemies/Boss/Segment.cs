

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

    private void Update()
    {
        Vector3 dir = transform.position - parent.position;
        float distance = dir.magnitude;

        if (distance > offSet)
        {

            Vector3 newPos = parent.position + -dir.normalized * offSet;
            rb.MovePosition(newPos);
        }

        // Rotation
        if (dir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(-dir);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, speed * Time.deltaTime));
        }
    }
}
