using UnityEngine;

public class TargetAnimator : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    private bool _isTargeting = false;

    private void OnEnable()
    {
        PlayerInputHandler.OnTarget += ToggleSelect;
        TargetFinder.OnLockOff += UnSelect;

    }

    private void OnDisable()
    {
        PlayerInputHandler.OnTarget -= ToggleSelect;
        TargetFinder.OnLockOff += UnSelect;
    }

    private void ToggleSelect()
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
                UnSelect();
            }
        }
    }

    private void UnSelect()
    {
        if(_animator != null)
        {
            _animator.SetBool("IsTargeting", false);
            _isTargeting = false;
        }
    }
}
