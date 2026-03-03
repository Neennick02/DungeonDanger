using UnityEngine;

public class BatEnemy : BaseEnemy
{
    protected override void Update()
    {
        base.Update();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if(health != null )
            {
                health.DrainHealth(enemyObject.Damage);
            }
        }
    }
}
