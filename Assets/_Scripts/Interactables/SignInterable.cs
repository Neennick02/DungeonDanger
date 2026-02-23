using UnityEngine;
using UnityEngine.Events;

public class SignInterable : BaseInteractable
{
    public UnityEvent CloseSign;
    private bool enabled = false;
    protected override void Start()
    {
        base.Start();
        interactionPopup = "Read";
    }

    public override void Interact()
    {
        if (!enabled)
        {
            base.Interact();
            enabled = true;
        }
        else
        {
            CloseSign.Invoke();
            enabled = false;
        }
        
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        CloseSign.Invoke();
        enabled = false;
    }
}
