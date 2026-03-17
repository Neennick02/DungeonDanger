using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandler : MonoBehaviour
{
    public InputActionAsset InputActions;

    private InputAction moveAction;
    private InputAction dodgeAction;

    private InputAction targetAction;
    private InputAction targetRight;
    private InputAction targetLeft;

    private InputAction ActionButtonAction;
    private InputAction AttackAction;
    private InputAction DefendAction;

    private InputAction PauseAction;

    public static event Action OnTarget;
    public static event Action OnDodge;
    public static event Action<Vector2> OnMove;
    public static event Action OnAction;
    public static event Action OnAttack;

    public static event Action OnDefendStart;
    public static event Action OnDefendEnd;

    public static event Action OnPause;



    private Vector2 moveInput;


    public static event Action<int> OnTargetRight;
    public static event Action<int> OnTargetLeft;
    private void Awake()
    {
        moveAction = InputActions.FindAction("Move");
        dodgeAction = InputActions.FindAction("Dodge");

        targetAction = InputActions.FindAction("Target");
        targetRight = InputActions.FindAction("TargetRight");
        targetLeft = InputActions.FindAction("TargetLeft");
        ActionButtonAction = InputActions.FindAction("ActionButton");
        AttackAction = InputActions.FindAction("Attack");
        DefendAction = InputActions.FindAction("Defend");

        PauseAction = InputActions.FindAction("Pause");
    }
    private void OnEnable()
    {
        InputActions.FindActionMap("PlayerActions").Enable();

        dodgeAction.Enable();
        dodgeAction.performed += OnDodgePerformed;

        ActionButtonAction.Enable();
        ActionButtonAction.performed += OnActionPerformed;

        AttackAction.Enable();
        AttackAction.performed += OnAttackPerformed;

        DefendAction.Enable();
        DefendAction.started += OnDefendStarted;

        DefendAction.canceled += OnDefendCancelled;

        targetAction.Enable();
        targetAction.performed += OnTargetPerformed;

        targetRight.Enable();
        targetRight.performed += OnTargetRightPerformed;

        targetLeft.Enable();
        targetLeft.performed += OnTargetLeftPerformed;

        PauseAction.Enable();
        PauseAction.performed += OnPausePerformed;
    }

    private void OnDisable()
    {
        dodgeAction.Disable();
        dodgeAction.performed -= OnDodgePerformed;
        OnDodge = null;

        ActionButtonAction.Disable();
        ActionButtonAction.performed -= OnActionPerformed;
        OnAction = null;

        AttackAction.Disable();
        AttackAction.performed -= OnAttackPerformed;
        OnAttack = null;

        DefendAction.Disable();
        DefendAction.started -= OnDefendStarted;
        DefendAction.canceled -= OnDefendCancelled;
        OnDefendStart = null;
        OnDefendEnd = null;

        targetAction.Disable();
        targetAction.performed -= OnTargetPerformed;
        OnTarget = null;

        targetRight.Disable();
        targetRight.performed -= OnTargetRightPerformed;
        OnTargetRight = null;

        targetLeft.Disable();
        targetLeft.performed -= OnTargetLeftPerformed;
        OnTargetLeft = null;    

        PauseAction.Disable();
        PauseAction.performed -= OnPausePerformed;
        OnPause = null;

        InputActions.FindActionMap("PlayerActions").Disable();
    }

    private void OnDodgePerformed(InputAction.CallbackContext ctx)
    {
        OnDodge?.Invoke();
    }
    private void OnActionPerformed(InputAction.CallbackContext ctx)
    {
        OnAction?.Invoke();
    }
    private void OnAttackPerformed(InputAction.CallbackContext ctx)
    {
        OnAttack?.Invoke();
    }

    private void OnDefendStarted(InputAction.CallbackContext ctx)
    {
        OnDefendStart?.Invoke();
    }

    private void OnDefendCancelled(InputAction.CallbackContext ctx)
    {
        OnDefendEnd?.Invoke();
    }
    private void OnTargetPerformed(InputAction.CallbackContext ctx)
    {
        OnTarget?.Invoke();
    }
    private void OnTargetRightPerformed(InputAction.CallbackContext ctx)
    {
        OnTargetRight?.Invoke(1);
    }
    private void OnTargetLeftPerformed(InputAction.CallbackContext ctx)
    {
        OnTargetLeft?.Invoke(-1);
    }
    private void OnPausePerformed(InputAction.CallbackContext ctx)
    {
        OnPause?.Invoke();
    }
    private void Update()
    {
        Vector2 move = moveAction.ReadValue<Vector2>();
        if (move != null)
        OnMove?.Invoke(move);
    }
}
