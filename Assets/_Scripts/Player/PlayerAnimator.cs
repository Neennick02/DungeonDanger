using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AnimationClip walkClip;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);

        if (speed < 0.3f)
        {
            animator.speed = 0.8f;
        }
        if (speed == 0)
        {
            animator.speed = 1;
        }
    }

    public void Flip()
    {
        animator.speed = 1;
        animator.SetBool("IsFlipping", true);
        StartCoroutine(ResetBool("IsFlipping"));
    }

    public void Roll()
    {
        animator.speed = 1;
        animator.SetBool("IsRolling", true);
        StartCoroutine(ResetBool("IsRolling"));
    }

    IEnumerator ResetBool(string name)
    {
        yield return new WaitForSeconds(1);
        animator.SetBool(name, false);
    }
}
