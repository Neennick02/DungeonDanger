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

    protected virtual void Start()
    {

        currentHealth = maxHealth;
        meshRenderer = GetComponent<MeshRenderer>();
        if(meshRenderer == null) meshRenderer = GetComponentInChildren<MeshRenderer>();
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
