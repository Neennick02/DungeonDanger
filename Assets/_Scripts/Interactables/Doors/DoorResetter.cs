using System.Collections;
using UnityEngine;

public class DoorResetter : MonoBehaviour
{
    [SerializeField] Animator _animator;
    public void ResetAnimator()
    {
        _animator.enabled = false;
    }
}
