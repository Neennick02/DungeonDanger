using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerInventory inventory;
    private void Start()
    {
        animator = GetComponent<Animator>();
        inventory = GetComponentInParent<PlayerInventory>();

        PlayerAttack.OnGrabSword += GrabSword;
    }


    public void SetSpeed(float speed, float x, float y)
    {
        animator.SetFloat("Speed", speed);
        animator.SetFloat("XSpeed", x, 0.1f, Time.deltaTime);
        animator.SetFloat("YSpeed", y, 0.1f, Time.deltaTime);
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
        if (inventory.swordInHand)
        {
            animator.speed = 1;
            animator.SetBool("IsAttacking", true);
            StartCoroutine(ResetBool("IsAttacking"));
        }
        else
        {
            GrabSword();
        }
    }

    public void GrabSword()
    {
        animator.SetBool("GrabSword", true);
        StartCoroutine(ResetBool("GrabSword"));
    }

    public void PutAway()
    {
        animator.SetBool("PutAway", true);
        StartCoroutine(ResetBool("PutAway"));
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
