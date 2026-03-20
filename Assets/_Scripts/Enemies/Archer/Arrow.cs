using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private EnemyObject enemySO;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.DrainHealth(enemySO.Damage);
                Destroy(gameObject);
            }
        }
    }
}
