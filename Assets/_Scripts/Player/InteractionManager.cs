using System;
using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : MonoBehaviour
{
    private BaseInteractable interactableScript;
    private PlayerInventory inventory;
    private PlayerMovement movement;
    public static event Action OnPutAway;


    public UnityEvent<string> popupEvent;
    private void Start()
    {
        inventory = GetComponent<PlayerInventory>();
        movement = GetComponent<PlayerMovement>();

        PlayerInput.OnAction += UseInteractable;
        popupEvent.Invoke("");
    }

    private void Update()
    {
        if(interactableScript == null && inventory.swordInHand&& !movement._isTargeting) 
        {
            popupEvent.Invoke("Put Away");
        }
        if(interactableScript == null && !inventory.swordInHand)
        {
            popupEvent.Invoke(" ");
        }
    }
    public void AddInteractable(BaseInteractable interactable)
    {
        interactableScript = interactable;
        popupEvent.Invoke(interactableScript.interactionPopup);
    }

    public void RemoveInteractable(BaseInteractable interactable)
    {
        interactableScript = null;
        popupEvent.Invoke(" ");
    }

    private void UseInteractable()
    {
        if (interactableScript != null)
        {
            //interact with object
            interactableScript.Interact();
        }

        if (inventory.swordInHand)
        {
            OnPutAway?.Invoke();  
        }

    }
}
