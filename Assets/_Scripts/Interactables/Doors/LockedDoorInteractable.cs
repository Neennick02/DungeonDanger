using System;
using Unity.VisualScripting;
using UnityEngine;

public class LockedDoorInteractable : DoorInteractable
{
    private PlayerInventory inventory;
    private bool isLocked = true;
    [SerializeField] private float endPoint;
    #region OnEnable
    private void OnEnable()
    {
        SaveStatueInteractable.OnSavePlayerData += SaveState;
        GameManager.OnLoad += LoadState;
    }

    private void OnDisable()
    {
        SaveStatueInteractable.OnSavePlayerData -= SaveState;
        GameManager.OnLoad -= LoadState;
    }
    #endregion
    protected override void Start()
    {
        base.Start();
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

    private void SaveState()
    {
        int locked = isLocked? 0 : 1;

        PlayerPrefs.SetInt("Locked" + transform.name, locked);
    }

    private void LoadState()
    {
        int locked = PlayerPrefs.GetInt("Locked", 1);

        isLocked = locked > 0 ? true : false;

        transform.position = new Vector3(0, endPoint, 0);
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
