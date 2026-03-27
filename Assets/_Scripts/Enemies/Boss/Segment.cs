

using UnityEngine;

public class Segment : MonoBehaviour
{
    private Rigidbody rb;
    private float startHeight;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startHeight = rb.position.y;
    }
    public void UpdatePosition(Vector3 pos, Vector3 parentPos)
    {
        Vector3 newPos = new Vector3(pos.x, startHeight, pos.z);
        rb.MovePosition(newPos);

        Vector3 dir = parentPos - newPos;
        if (dir.sqrMagnitude > 0.0001f)
        {
            rb.MoveRotation(Quaternion.LookRotation(dir));
        }
    }
}
