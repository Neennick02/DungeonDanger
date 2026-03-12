using System;
using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : MonoBehaviour
{
    private BaseInteractable interactableScript;
    public static event Action OnPutAway;


    public UnityEvent<string> popupEvent;
    private void Start()
    {

        PlayerInputHandler.OnAction += UseInteractable;
        popupEvent.Invoke("");
    }

    private void Update()
    {
        if(interactableScript == null)
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
    }

    public void EmptyInteractable()
    {
        interactableScript = null;
        popupEvent.Invoke(" ");
    }
}
