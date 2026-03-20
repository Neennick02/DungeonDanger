using UnityEngine;

public class Segment : MonoBehaviour
{
    private float speed = 5;
    [SerializeField] private bool isTail = false;
    [SerializeField] private GameObject parent;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.MovePosition(Vector3.Lerp(transform.position, parent.transform.position, Time.deltaTime * speed));
    }

}
