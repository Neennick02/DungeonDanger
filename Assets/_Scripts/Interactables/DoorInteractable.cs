using UnityEngine;

public class DoorInteractable : BaseInteractable
{
    private Animator _animator;

    protected override void Start()
    {
        base.Start();
        interactionPopup = "Press action to open.";
        _animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        base.Interact();
    }

    public void ResetAnimator()
    {
        _animator.enabled = false;
    }
}
