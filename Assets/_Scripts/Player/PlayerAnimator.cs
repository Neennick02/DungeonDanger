using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerInventory inventory;
    public bool isAttacking;
    private void Start()
    {
        animator = GetComponent<Animator>();
        inventory = GetComponentInParent<PlayerInventory>();

        PlayerAttack.OnGrabSword += GrabSword;
    }
    private void OnEnable()
    {
        PlayerHealth.OnDeath += IsDead;
    }

    private void OnDisable()
    {
        PlayerHealth.OnDeath -= IsDead;
    }
    public void SetSpeed(float speed, float x, float y)
    {
        if (animator == null) return;
        animator.SetFloat("Speed", speed);
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
    public void Attack()
    {
        if (animator == null) return;

        if (inventory.swordInHand)
        {
            animator.speed = 1;
            animator.SetTrigger("IsAttacking");
        }
        else
        {
            GrabSword();
        }
    }

    public void GrabSword()
    {
        if (animator == null) return;

        animator.SetBool("GrabSword", true);
        StartCoroutine(ResetBool("GrabSword"));
    }

    public void PutAway()
    {
        if (animator == null) return;

        animator.SetBool("PutAway", true);
        StartCoroutine(ResetBool("PutAway"));
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
