using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
    public InputActionAsset InputActions;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction dodgeAction;

    private Vector2 moveAmount;
    private Vector2 lookAmount;


    private void OnEnable()
    {
        InputActions.FindActionMap("PlayerInputActions").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("PlayerInputActions").Disable();
    }

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        dodgeAction = InputSystem.actions.FindAction("Dodge");

    }

}
