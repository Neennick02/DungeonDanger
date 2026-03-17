using System;
using UnityEngine;

public class SaveStatueInteractable : BaseInteractable
{
    [SerializeField] private int price = 1;
    public static event Action OnSavePlayerData;

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
            inventory.UpdateCoinAmount(-price);
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


}
