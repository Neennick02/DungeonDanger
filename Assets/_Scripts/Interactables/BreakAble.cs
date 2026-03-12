using System.Collections.Generic;
using UnityEngine;

public class BreakAble : BaseHealth
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            DropItems();
            Die();
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (isFalling)
        {
            float fallDist = highestPoint - transform.position.y;

            if (fallDist > minFallHeight)
            {
                DropItems();
                Die();
            }
            isFalling = false;
        }
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}
