using System;
using System.Collections;
using UnityEngine;

public class SaveStatueInteractable : BaseInteractable
{
    [SerializeField] private int price = 1;
    public static event Action OnSavePlayerData;

    [SerializeField] private GameObject effect;
    private PlayerInventory inventory;

    private void OnEnable()
    {
        GameManager.OnSave += Save;
    }

    private void OnDisable()
    {
        GameManager.OnSave -= Save;
    }

    private void Save()
    {
        OnSavePlayerData?.Invoke();

    }
    public override void Interact()
    {
        if (inventory.coinAmount >= price && inventory != null)
        {
            OnInteract?.Invoke();
            inventory.UpdateCoinAmount(-price);
            price = 0;
            Save(); 
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionManager manager = other.GetComponent<InteractionManager>();
            manager.AddInteractable(this);

            inventory = manager.GetComponent<PlayerInventory>();
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionManager manager = other.GetComponent<InteractionManager>();
            manager.RemoveInteractable(this);

            inventory = null;
        }
    }
    public void ResetEffect()
    {
        StartCoroutine(TurnOffParticles());
    }
    IEnumerator TurnOffParticles()
    {
        if (effect != null)
        {
            yield return new WaitForSeconds(2);
            effect.GetComponent<ParticleSystem>().Stop();
            yield return new WaitForSeconds(1);

            effect.SetActive(false);
        }

    }
}
