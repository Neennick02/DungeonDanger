using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Collider swordModel;


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
            swordModel.enabled = true;
        }
        else
        {
            swordModel.enabled = false;
        }

        attackCoolDown -= Time.deltaTime;

        if(attackCoolDown < 0f)
        {
            comboCounter = 0;
        }
    }

    private void Attack()
    {
        if (!movement._characterController.isGrounded) return;

        if (!isAttacking)
        {
            isAttacking = true;
            
            //reset combo
            if (comboCounter > 1)
            {
                comboCounter = 0;
            }

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

            attackCoolDown += 1;
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
