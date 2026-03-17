using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    [SerializeField] private float delay = 1f;
    private float timer;
    private bool hit = false;   
    private void Update()
    {

        timer += Time.deltaTime;

            if (timer >= delay){ 
                hit = false;
            }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")){

            var enemyHealth = other.gameObject.GetComponent<BaseHealth>();
            if (enemyHealth == null) enemyHealth = other.gameObject.GetComponentInParent<BaseHealth>();

            if (enemyHealth != null && !hit)
            {
                
                enemyHealth.DrainHealth(damage);
                timer = 0f;
                hit = true;
            }
        }
    }
}
