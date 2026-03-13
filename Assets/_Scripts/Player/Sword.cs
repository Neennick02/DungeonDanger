using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    [SerializeField] private float delay = 1f;
    private bool hit = false;   
    private void Update()
    {
        if (hit)
        {
            delay -= Time.deltaTime;
            if (delay <= 0f){ 
                hit = false;
                delay = 1f;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")){

            var enemyHealth = other.gameObject.GetComponent<BaseHealth>();
            if (enemyHealth == null) enemyHealth = other.gameObject.GetComponentInParent<BaseHealth>();

            if (enemyHealth != null)
            {
                if (delay > 0f)
                {
                    enemyHealth.DrainHealth(damage);
                }
                hit = true;
            }
        }
    }
}
