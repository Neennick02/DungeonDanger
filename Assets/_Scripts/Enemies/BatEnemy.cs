using UnityEngine;

public class BatEnemy : BaseEnemy
{
    private PlayerHealth health;
    protected override void Update()
    {
        base.Update();

        timer += Time.deltaTime;

        if(timer > enemyObject.AttackInterval && health != null)
        {  
            timer = 0f;
            health.DrainHealth(enemyObject.Damage);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            health = other.GetComponent<PlayerHealth>();
            if (health == null) health = other.GetComponentInParent<PlayerHealth>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            health = null;
        }
    }
}
