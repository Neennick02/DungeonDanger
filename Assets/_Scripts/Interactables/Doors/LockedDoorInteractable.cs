using UnityEngine;

public class LockedDoorInteractable : DoorInteractable
{
    private PlayerInventory inventory;
    private bool isLocked = true;
    protected override void Start()
    {
        base.Start();
        interactionPopup = "Press action to open.";
        _animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (isLocked)
        {
            //only open when enough keys 
            if (inventory.keyAmount > 0)
            {
                //remove keys and disable lock
                inventory.UpdateKeyAmount(-1);
                isLocked = false;

                //change popup
                interactionPopup = "Open";
                
                //open door
                OnInteract?.Invoke();
            }
        }
        else
        {
            //if door is unlocked always open
            OnInteract?.Invoke();
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        //get the inventory
        if (other.CompareTag("Player"))
        {
            inventory = other.GetComponent<PlayerInventory>();
        }
    }
}
