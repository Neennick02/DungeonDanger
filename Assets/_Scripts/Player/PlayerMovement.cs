using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [Header("Movement")]
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _movementAcceleration = 10f;
    private Vector3 _velocity;
    private float _verticalVelocity;

    [Header("Jump")]
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpForce = 10;
    private bool _isJumping;
    private bool _wasGrounded;
    private void OnEnable()
    {
        PlayerInput.OnMove += Move;
        PlayerInput.OnDodge += Dodge;
    }

    private void OnDisable()
    {
        PlayerInput.OnMove -= Move;
        PlayerInput.OnDodge -= Dodge;

    }

    private void Move(Vector2 input)
    {
        //get camera position
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        //convert to 3d space
        Vector3 move = camForward * input.y + camRight * input.x;

        _velocity = Vector3.MoveTowards(
        _velocity, move, _movementAcceleration * Time.deltaTime);

        if(_characterController.isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = -2;
        }
        else
        {
            _verticalVelocity += _gravity * Time.deltaTime;
        }

        Vector3 finalMovement = _velocity * _movementSpeed;
        finalMovement.y = _verticalVelocity;

        _characterController.Move(finalMovement * _movementSpeed * Time.deltaTime);

    }

    private void Update()
    {
        CheckForLedges();

        _wasGrounded = _characterController.isGrounded;
    }

    private void CheckForLedges()
    {
        bool grounded = _characterController.isGrounded;

        if(_wasGrounded && !grounded && !_isJumping)
        {
            Jump();
        }

        if (grounded) _isJumping = false;
    }

    private void Jump()
    {
        if (!_isJumping)
        {
            _verticalVelocity = _jumpForce;
            _isJumping = true;
        }
    }

    private void Dodge()
    {

    }
}
