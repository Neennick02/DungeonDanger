using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    public bool isAttacking;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        PlayerHealth.OnDeath += IsDead;
        PlayerInputHandler.OnDefendStart += StartDefend;
        PlayerInputHandler.OnDefendEnd += EndDefend;
        PlayerInventory.OnForceEndShield += EndDefend;
    }

    private void OnDisable()
    {
        PlayerHealth.OnDeath -= IsDead;
        PlayerInputHandler.OnDefendStart -= StartDefend;
        PlayerInputHandler.OnDefendEnd -= EndDefend;
        PlayerInventory.OnForceEndShield -= EndDefend;
    }
    public void SetSpeed(float speed, float x, float y)
    {
        if (animator == null) return;
        animator.SetFloat("Speed", speed, 0.1f,  Time.deltaTime);
        animator.SetFloat("XSpeed", x, 0.1f, Time.deltaTime);
        animator.SetFloat("YSpeed", y, 0.1f, Time.deltaTime);
    }
    public void IsTargeting(bool isTargeting)
    {
        if (animator == null) return;
        animator.SetBool("IsTargeting", isTargeting);
    }
    public void IsGrounded(bool isGrounded)
    {
        if (animator == null) return;

        if (!isAttacking)
        {
            animator.SetBool("IsGrounded", isGrounded);
        }
        else
        {
            animator.SetBool("IsGrounded", true);
        }
    }

    public void IsDead()
    {
        if (animator == null) return;

        animator.SetTrigger("Die");
    }

    public void Flip()
    {
        if (animator == null) return;

        animator.speed = 1;
        animator.SetBool("IsFlipping", true);
        StartCoroutine(ResetBool("IsFlipping"));
    }
    public void Attack(int combo)
    {
        if (animator == null) return;

        animator.SetTrigger("IsAttacking");

        /*        switch (combo) 
                {
                    case 0:
                    animator.SetTrigger("IsAttacking");
                    break;

                    case 1:
                        animator.SetTrigger("IsAttacking 0");
                        break;
                }*/
    }

    public void StartDefend()
    {
        animator.SetBool("IsDefending", true);
        animator.SetLayerWeight(0, 0);
        animator.SetLayerWeight(1, 1);
    }
    public void EndDefend()
    {
        StartCoroutine(EndDefendRoutine());
    }

    IEnumerator EndDefendRoutine()
    {
        animator.SetBool("IsDefending", false);

        yield return new WaitForSeconds(0.5f);
        animator.SetLayerWeight(0, 1);
        animator.SetLayerWeight(1, 0);
    }
    public void Roll()
    {
        if (animator == null) return;

        animator.speed = 1;
        animator.SetBool("IsRolling", true);
        StartCoroutine(ResetBool("IsRolling"));
    }
    public void Hop()
    {
        if (animator == null) return;

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
