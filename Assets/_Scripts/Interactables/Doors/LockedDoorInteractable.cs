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
        PlayerPrefs.SetInt("Locked" + transform.name, isLocked ? 1 : 0);
    }

    private void LoadState()
    {
        int locked = PlayerPrefs.GetInt("Locked" + transform.name, 1);

        isLocked = locked == 1;

        if (!isLocked)
        {
            transform.position = new Vector3(0, endPoint, 0);
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
