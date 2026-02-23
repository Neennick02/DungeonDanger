using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class OrderedOnOffInteractable : OnOffInteractable
{
    public UnityEvent<int> OnLit;
    public int TorchIndex;
    protected override void Start()
    {
        base.Start();
        isLit = false;
        keepOn = false;
    }

    public override void Interact()
    {
        if (isLit) return;

        OnInteract?.Invoke();
        isLit = true;
        OnLit?.Invoke(TorchIndex);

        StopAllCoroutines();
    }

    public void KeepOn()
    {
        keepOn = true;
        Interact();
    }

    public void TurnOff()
    {
        OffInteract?.Invoke();
        isLit = false;
    }
}
