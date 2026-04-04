using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
public abstract class BaseHealth : MonoBehaviour
{
    [SerializeField] private EnemyObject enemySo;
    protected int maxHealth;
    protected int currentHealth;
    protected bool isDead = false;
    public static event Action SwitchTarget;
    protected Transform targetTransform;

    [SerializeField] protected List<Renderer> meshes = new List<Renderer>();
    protected Material standardMat;
    [SerializeField] private Material DamageMat;
    [SerializeField] protected GameObject[] itemArray;

    [Header("Fall Damage config")]
    protected Rigidbody rb;
    protected bool isFalling;
    protected float highestPoint;

    public float minFallHeight = 5f;
    public float FallDamageMultiplier = 2;
    [SerializeField] protected GameObject deathEffect;
    protected virtual void Start()
    {
        maxHealth = enemySo.MaxHealth;
        currentHealth = maxHealth;
        targetTransform = transform;

        rb = GetComponent<Rigidbody>();

        //choose first material
        if(meshes.Count > 0)
        standardMat = meshes[0].material;

    }

    public virtual void DrainHealth(int amount)
    {
        if(isDead) return;

        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Die();
            isDead = true;
        }
    }
    public virtual void AddHealth(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
    }
    protected virtual void Update()
    {
        //handle fall detection
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
        //handle landing + falldamage
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
        isDead = true;
        TargetFinder.RemoveFromPool(targetTransform);
        SwitchTarget?.Invoke();
        DropItems();
    }
    protected void DropItems()
    {
        if (itemArray.Length == 0) return;

            int randomInt = UnityEngine.Random.Range(0, itemArray.Length);
            GameObject dropObject = itemArray[randomInt];

        if (dropObject != null)
        {
            Instantiate(dropObject, transform.position, Quaternion.identity);
        }
      }
    public void FlashRed()
    {
        StartCoroutine(Flash());
    }

    protected void SwitchTargetEvent()
    {
        SwitchTarget?.Invoke();
    }

    IEnumerator Flash()
    {
        if (meshes == null) Debug.Log("No mesh");

        for (int i = 0; i < meshes.Count; i++)
        {

            meshes[i].material = DamageMat;
        }

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < meshes.Count; i++)
        {

            meshes[i].material = standardMat;
        }

    }

    protected virtual IEnumerator DestroyAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        if (deathEffect != null)
        {
            deathEffect.SetActive(true);
            deathEffect.transform.eulerAngles = new Vector3(-90, 0, 0);
            deathEffect.transform.parent = null;
        }
        DropItems();    
        Destroy(gameObject);
    }

}
