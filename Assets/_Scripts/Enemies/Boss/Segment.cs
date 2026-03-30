

using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField] private BossObject bossObject;
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            health.DrainHealth(bossObject.Damage);
        }
    }
}
