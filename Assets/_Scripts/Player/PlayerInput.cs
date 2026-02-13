using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
    public InputActionAsset InputActions;

    private InputAction moveAction;
    private InputAction dodgeAction;
    private InputAction targetAction;
    private InputAction targetRight;
    private InputAction targetLeft;

    private Vector2 moveAmount;
    private Vector2 lookAmount;

    public static event Action OnTarget;
    public static event Action OnDodge;
    public static event Action OnTargetRight;
    public static event Action OnTargetLeft;

    private void OnEnable()
    {
        InputActions.FindActionMap("PlayerActions").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("PlayerActions").Disable();
    }

    private void Awake()
    {
        moveAction = InputActions.FindAction("Move");
        dodgeAction = InputActions.FindAction("Dodge");
        targetAction = InputActions.FindAction("Target");
        targetRight = InputActions.FindAction("TargetRight");
        targetLeft = InputActions.FindAction("TargetLeft");


        //subscribe to action performed callbacks
        if (targetAction != null)
            targetAction.performed += ctx => OnTarget?.Invoke();

        if (targetRight != null)
            targetRight.performed += ctx => OnTargetRight?.Invoke();

        if (targetLeft != null)
            targetLeft.performed += ctx => OnTargetLeft?.Invoke();


        if (dodgeAction != null)
            dodgeAction.performed += ctx => OnDodge?.Invoke();

    }
}
