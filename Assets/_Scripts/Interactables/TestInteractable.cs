using UnityEngine;

public class TestInteractable : BaseInteractable
{
    protected override void Start()
    {
        base.Start();
        interactionPopup = "Press E to change color";
    }
    public override void Interact()
    {
        base.Interact();
    }
}
