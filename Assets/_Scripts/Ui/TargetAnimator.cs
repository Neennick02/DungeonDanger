using UnityEngine;

public class TargetAnimator : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    private bool _isTargeting = false;

    private void OnEnable()
    {
        PlayerInput.OnTarget += SelectAndUnselect;

    }

    private void OnDisable()
    {
        PlayerInput.OnTarget -= SelectAndUnselect;
    }

    private void SelectAndUnselect()
    {
        //only if targets are in pool
        if (TargetFinder.pool.Count > 0)
        {
            //if already targeting than untarget
            if (!_isTargeting)
            {
                _animator.SetBool("IsTargeting", true);
                _isTargeting = true;
            }
            //if not targeting target
            else if (_isTargeting)
            {
                _animator.SetBool("IsTargeting", false);
                _isTargeting = false;
            }
        }
    }
}
