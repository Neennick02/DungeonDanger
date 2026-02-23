using UnityEngine;
using UnityEngine.Events;

public abstract class BaseInteractable : MonoBehaviour
{
    public string interactionPopup;
    public UnityEvent OnInteract;

    protected virtual void Start()
    {
        if (GetComponent<Collider>() == null)
            Debug.LogWarning("No collider or trigger added to " + transform.name);
    }
    public virtual void Interact()
    {
        OnInteract.Invoke();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionManager manager = other.GetComponent<InteractionManager>();
            manager.AddInteractable(this);
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionManager manager = other.GetComponent<InteractionManager>();
            manager.RemoveInteractable(this);
        }
    }
}
