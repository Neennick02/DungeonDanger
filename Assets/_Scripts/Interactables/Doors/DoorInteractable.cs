using System.Collections;
using System.Xml;
using UnityEngine;

public class DoorInteractable : BaseInteractable
{
    protected Animator _animator;
    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        base.Interact();
        StartCoroutine(ResetAnimator());
    }
    IEnumerator ResetAnimator()
    {
        if (_animator == null) yield break;
        yield return new WaitForSeconds(1.5f);
        _animator.enabled = false;
    }
}
