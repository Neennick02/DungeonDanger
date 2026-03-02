using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform model;
    [SerializeField] private float dashStartAmount = 0f;
    [SerializeField] private float dashEndAmount = 5f;
    private float dashAmount;

    [SerializeField] private float attackDuration = 1.5f;
    private float attackTimer  = 0;
    [SerializeField] private PlayerAnimator animator;
    private PlayerMovement movement;
    private PlayerMovement.PlayerState previousState;
    private CharacterController controller;
    private bool isAttacking;
    private void OnEnable()
    {
        PlayerInput.OnAttack += Attack;
        movement = GetComponent<PlayerMovement>();
        controller = GetComponent<CharacterController>();
    }

    private void OnDisable()
    {
        PlayerInput.OnAttack -= Attack;

    }
    private void Update()
    {
        if (isAttacking)
        {
            controller.Move(model.forward * dashAmount * Time.deltaTime);
        }
        

    }

    private void Attack()
    {
        if (!isAttacking)
        {
            previousState = movement.State;
            movement.State = PlayerMovement.PlayerState.Attacking;
            isAttacking = true;
            attackTimer = 0;
            animator.Attack();
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        while(attackTimer < attackDuration)
        {
            attackTimer += Time.deltaTime;
            dashAmount = Mathf.Lerp(dashStartAmount, dashEndAmount, attackTimer / attackDuration);

            yield return null;
        }
        movement.State = previousState;
        isAttacking = false;
        dashAmount = dashStartAmount;
    }


}
