using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : MonoBehaviour
{
    private bool interactableAvailable = false;
    private BaseInteractable interactableScript;
    public UnityEvent<string> popupEvent;

    private void Start()
    {
        PlayerInput.OnAction += UseInteractable;
        popupEvent.Invoke("");
    }
    public void AddInteractable(BaseInteractable interactable)
    {
        interactableAvailable = true;
        interactableScript = interactable;
        popupEvent.Invoke(interactableScript.interactionPopup);
    }

    public void RemoveInteractable(BaseInteractable interactable)
    {
        interactableAvailable = false;
        interactableScript = null;
        popupEvent.Invoke(" ");
    }

    private void UseInteractable()
    {
        if (interactableScript == null) return;

        interactableScript.Interact();
    }
}
