using UnityEngine;

public class WarriorAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void UpdateSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    public void EnableTrigger(string name)
    {
        animator.SetTrigger(name);
    }
}
