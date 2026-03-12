using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform model;
    [SerializeField] private Collider sword;


    [SerializeField] private float attackDuration = 1.5f;
    [SerializeField] private PlayerAnimator animator;

    private PlayerMovement movement;
    private PlayerMovement.PlayerState previousState;

    private bool isAttacking;
    private float attackTimer;
    private int comboCounter = 0;

    [SerializeField] private float attackCoolDown = 1f;
    private void OnEnable()
    {
        PlayerInputHandler.OnAttack += Attack;
    }

    private void OnDisable()
    {
        PlayerInputHandler.OnAttack -= Attack;

    }

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {

        if (isAttacking)
        {
            sword.enabled = true;
        }
        else
        {
            sword.enabled = false;
        }

        attackCoolDown -= Time.deltaTime;
    }

    private void Attack()
    {
        if (!movement._characterController.isGrounded) return;

        if (!isAttacking)
        {
            isAttacking = true;
            animator.Attack(comboCounter);

            animator.isAttacking = false;
            StopAllCoroutines();

            animator.isAttacking = true;
            movement.RotateCharacter(movement.move);

            //save state
            previousState = movement.State;

            //change state
            movement.State = PlayerMovement.PlayerState.Attacking;
            attackTimer = 0;

            //reset
            StartCoroutine(AttackRoutine());

            //increase combo
            comboCounter++;
        }
    }

    IEnumerator AttackRoutine()
    {
        while(attackTimer < attackDuration)
        {
            attackTimer += Time.deltaTime;


            yield return null;
        }
        movement.State = previousState;
        isAttacking = false;
        animator.isAttacking = false;

        attackCoolDown = 1f;
    }


}
