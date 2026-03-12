using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public abstract class BaseHealth : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected int currentHealth;
    protected bool isDead = false;
    public static event Action SwitchTarget;

    protected MeshRenderer meshRenderer;
    private Color DamageColor = Color.red;
    [SerializeField] protected GameObject[] itemArray;

    [Header("Fall Damage config")]
    protected Rigidbody rb;
    protected bool isFalling;
    protected float highestPoint;

    public float minFallHeight = 5f;
    public float FallDamageMultiplier = 2;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        meshRenderer = GetComponent<MeshRenderer>();
        if(meshRenderer == null) meshRenderer = GetComponentInChildren<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    public virtual void DrainHealth(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Die();
            isDead = true;
        }
    }
    public virtual void AddHealth(int amount)
    {
        currentHealth += amount;
    }
    protected virtual void Update()
    {
        if (rb == null) return;

        if (rb.linearVelocity.y < 0)
        {
            if (!isFalling)
            {
                isFalling = true;
                highestPoint = transform.position.y;
            }
        }
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (isFalling)
        {
            float fallDist = highestPoint - transform.position.y;

            if(fallDist > minFallHeight)
            {
                float damage = (fallDist * minFallHeight) * FallDamageMultiplier;
                  
            }
            isFalling = false;
        }
    }

    protected virtual void Die()
    {
            TargetFinder.RemoveFromPool(transform);
            SwitchTarget?.Invoke();
            DropItems();

            Destroy(gameObject);
    }
    protected void DropItems()
    {
            int randomInt = UnityEngine.Random.Range(0, itemArray.Length);
            GameObject dropObject = itemArray[randomInt];

        if (dropObject != null)
        {
            Instantiate(dropObject, transform.position, Quaternion.identity);
        }
            Destroy(gameObject);
      }
    public void FlashRed()
    {
        StopAllCoroutines();
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        if (meshRenderer == null) yield break;

        Color standard = meshRenderer.material.color;
        meshRenderer.material.color = DamageColor;
        yield return new WaitForSeconds(0.1f);
        meshRenderer.material.color = standard;
    }

}
