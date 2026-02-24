using UnityEngine;
using UnityEngine.Events;

public class SignInteractable : BaseInteractable
{
    public UnityEvent CloseSign;
    private bool _enabled = false;
    protected override void Start()
    {
        base.Start();
        interactionPopup = "Read";
    }

    public override void Interact()
    {
        if (!_enabled)
        {
            base.Interact();
            _enabled = true;
        }
        else
        {
            CloseSign.Invoke();
            _enabled = false;
        }
        
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        CloseSign.Invoke();
        _enabled = false;
    }
}
