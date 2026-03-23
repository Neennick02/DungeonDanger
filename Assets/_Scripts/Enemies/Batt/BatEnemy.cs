using Unity.VisualScripting;
using UnityEngine;

public class BatEnemy : BaseEnemy
{
    private PlayerHealth health;
    [SerializeField] private Animator flyAnimator, wingAnimator;
    protected override void Update()
    {
        if (isDead) return;

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
        if(isDead) return;

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

    public override void Die()
    {
        isDead = true;
        
        flyAnimator.enabled = false;
        wingAnimator.enabled = false;

        Destroy(agent);
        transform.AddComponent<Rigidbody>();
    }
}
