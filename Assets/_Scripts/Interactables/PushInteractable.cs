using UnityEngine;
using UnityEngine.Events;

public class PushInteractable : BaseInteractable
{
    public bool _isPushing { get; private set; }
    public UnityEvent OffInteract;

    public override void Interact()
    {
        if (!_isPushing)
        {
            OnInteract?.Invoke();
            PlayerMovement.pushAbleBlockObject = gameObject;
        }
        else
        {
            OffInteract?.Invoke();
        }
        _isPushing = !_isPushing;
    }

    public void StopPushing()
    {
        _isPushing = false;
    }
}
