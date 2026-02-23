using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OnOffInteractable : BaseInteractable
{
    [SerializeField] protected float resetDelay = 5f;
    public UnityEvent OffInteract;
  
    public bool isLit = false;
    protected bool keepOn = false;
    protected override void Start()
    {
        base.Start();
        interactionPopup = "Light";
    }

    public override void Interact()
    {
        base.Interact();
        isLit = true;
        StopAllCoroutines();
        
        if(!keepOn)
        StartCoroutine(ResetInteractable());
    }

    IEnumerator ResetInteractable()
    {
        yield return new WaitForSeconds(resetDelay);
        OffInteract?.Invoke();
        isLit = false;
    }

    public void KeepOn()
    {
        keepOn = true;
        Interact();
    }
}
