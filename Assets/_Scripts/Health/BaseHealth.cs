using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public abstract class BaseHealth : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected int currentHealth;
    protected bool isDead = false;
    public UnityEvent SwitchTarget;

    protected MeshRenderer meshRenderer;
    private Color DamageColor = Color.red;
    [SerializeField] protected GameObject[] itemArray;

    protected virtual void Start()
    {

        currentHealth = maxHealth;
        meshRenderer = GetComponent<MeshRenderer>();
        if(meshRenderer == null) meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    protected virtual void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        CheckHealth();
    }

    public virtual void DrainHealth(int amount)
    {
        currentHealth -= amount;
    }
    public virtual void AddHealth(int amount)
    {
        currentHealth += amount;
    }

    protected virtual void CheckHealth()
    {
        if(currentHealth <= 0 && !isDead)
        {
            Die();
            isDead = true;
        }
    }
    protected virtual void Die()
    {
        //add code to sub classes
            TargetFinder.RemoveFromPool(transform);
            SwitchTarget?.Invoke();
            DropItems();
            Destroy(gameObject);
    }
    protected void DropItems()
    {
            int randomInt = Random.Range(0, itemArray.Length);
            GameObject dropObject = itemArray[randomInt];

            Instantiate(dropObject, transform.position, Quaternion.identity);
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
