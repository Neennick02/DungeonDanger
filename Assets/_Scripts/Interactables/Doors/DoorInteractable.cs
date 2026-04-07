using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class DoorInteractable : BaseInteractable
{
    protected Animator _animator;
    [SerializeField] private List<AudioClip> doorSounds;
    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        base.Interact();
        AudioManager.Instance.PlayClip(doorSounds);
        StartCoroutine(ResetAnimator());
    }
    IEnumerator ResetAnimator()
    {
        if (_animator == null) yield break;
        yield return new WaitForSeconds(1.5f);
        _animator.enabled = false;
    }
}
