using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStatueInteractable : BaseInteractable
{
    [SerializeField] private int price = 1;
    public static event Action OnSavePlayerData;

    [SerializeField] private GameObject effect;
    private PlayerInventory inventory;
    [SerializeField] private List<AudioClip> saveSound;

    private void OnEnable()
    {
        GameManager.OnSave += Save;
        interactionPopup = "Save : " + price + " coins";
    }

    private void OnDisable()
    {
        GameManager.OnSave -= Save;
    }

    private void Save()
    {
        OnSavePlayerData?.Invoke();
        AudioManager.Instance.PlayClip(saveSound);
    }
    public override void Interact()
    {
        if (inventory.coinAmount >= price && inventory != null)
        {
            OnInteract?.Invoke();
            inventory.UpdateCoinAmount(-price);
            price = 0;
            interactionPopup = "Save : " + price + " coins";
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
        ParticleSystem system = effect.GetComponent<ParticleSystem>();
        if (effect != null && system != null) ;
        {
            yield return new WaitForSeconds(2);
            system.Stop();
            yield return new WaitForSeconds(1);

            effect.SetActive(false);
        }

    }
}
