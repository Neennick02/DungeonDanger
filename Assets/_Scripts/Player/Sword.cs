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
            Debug.Log("Hit object: " + other.name);
            Debug.Log("Root object: " + other.transform.root.name);
            var enemyHealth = other.gameObject.GetComponent<BaseHealth>();

            if (enemyHealth != null)
            {
                if (delay > 0f)
                {
                    enemyHealth.DrainHealth(damage);
                }
                hit = true;

                Debug.Log("Damaged " + other.name);
            }
        }
    }
}
