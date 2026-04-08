using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Collider swordModel;


    [SerializeField] private float attackDuration;
    [SerializeField] private float attackCoolDown;

    [SerializeField] private PlayerAnimator animator;

    private PlayerMovement movement;
    private PlayerMovement.PlayerState previousState;

    private bool isAttacking;
    private float attackTimer;
    private int comboCounter = 0;

    [SerializeField] private List<AudioClip> swordSwingSounds;
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
        attackTimer += Time.deltaTime;
    }

    private void Attack()
    {
        //ground check
        if (!movement._characterController.isGrounded) return;

        //only attack once
        if (!isAttacking)
        {
            if (attackTimer < attackCoolDown) return;

            AudioManager.Instance.PlayClip(swordSwingSounds);
            isAttacking = true;
            
            /*//reset combo
            if (comboCounter > 1)
            {
                comboCounter = 0;
            }*/

            //set animation
            animator.Attack(comboCounter);

            animator.isAttacking = true;
            movement.RotateCharacter(movement.move);

            //save state
            previousState = movement.State;

            //change state
            movement.State = PlayerMovement.PlayerState.Attacking;
            attackTimer = 0;

            //reset
            StopAllCoroutines();
            StartCoroutine(AttackRoutine());

            //increase combo
            comboCounter++;
        }
    }

    IEnumerator AttackRoutine()
    {
        float quaterDuration = attackDuration / 4;
        yield return new WaitForSeconds(quaterDuration);
        swordModel.enabled = true;
        yield return new WaitForSeconds(quaterDuration * 3);


        movement.State = previousState;
        isAttacking = false;
        animator.isAttacking = false;
        swordModel.enabled = false;
        attackTimer = attackCoolDown;
    }


}
