using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
  //  [SerializeField] private AnimationClip walkClip;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void SetSpeed(float speed, float x, float y)
    {
        animator.SetFloat("Speed", speed);
        animator.SetFloat("XSpeed", x, 0.1f, Time.deltaTime);
        animator.SetFloat("YSpeed", y, 0.1f, Time.deltaTime);

        /*        if (speed < 0.3f)
                {
                    animator.speed = 0.8f;
                }
                if (speed == 0)
                {
                    animator.speed = 1;
                }*/
    }
    public void IsTargeting(bool isTargeting)
    {
        animator.SetBool("IsTargeting", isTargeting);
    }
    public void IsGrounded(bool isGrounded)
    {
        animator.SetBool("IsGrounded", isGrounded);
    }

public void Flip()
    {
        animator.speed = 1;
        animator.SetBool("IsFlipping", true);
        StartCoroutine(ResetBool("IsFlipping"));
    }
    public void Attack()
    {
        animator.speed = 1;
        animator.SetBool("IsAttacking", true);
        StartCoroutine(ResetBool("IsAttacking"));
    }

    public void Roll()
    {
        animator.speed = 1;
        animator.SetBool("IsRolling", true);
        StartCoroutine(ResetBool("IsRolling"));
    }
    public void Hop()
    {
        animator.speed = 1;
        animator.SetBool("IsHopping", true);
        StartCoroutine(ResetBool("IsHopping"));
    }

    IEnumerator ResetBool(string name)
    {
        yield return new WaitForSeconds(1);
        animator.SetBool(name, false);
    }
}
