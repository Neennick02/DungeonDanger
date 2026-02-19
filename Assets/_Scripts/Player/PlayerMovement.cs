using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [Header("Movement")]
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _movementAcceleration = 10f;
    private Vector3 _velocity;
    private float _verticalVelocity;

    [Header("Jump")]
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpForce = 10;
    private bool _isJumping;
    private bool _wasGrounded;

    [Header("Dodge")]
    [SerializeField] private float _dodgeForce = 10;
    [SerializeField] private float _dodgeDuration = 0.2f;
    [SerializeField] private float _dodgeCooldown = 1f;
    private Vector3 _dodgeDirection;
    private float _dodgeTime;
    private float _dodgeCooldownTimer;
    private bool _isDodging;

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

        //rotate character in move direction
        if(move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);

            transform.rotation = Quaternion.Slerp(
            transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        if (_isDodging)
        {
            _dodgeTime -= Time.deltaTime;

            if(_dodgeTime > 0)
            {
                _velocity = _dodgeDirection * _dodgeForce;
            }
            else
            {
                _isDodging = false;
            }
        }
        else
        {
            _velocity = Vector3.MoveTowards(
            _velocity, move, _movementAcceleration * Time.deltaTime);
        }

        //add gravity
        if(_characterController.isGrounded && _verticalVelocity < 0)
        {
            //less gravity when grounded
            _verticalVelocity = -2;
        }
        else
        {
            //normal amount
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

        if(_dodgeCooldownTimer > 0)
        {
            _dodgeCooldownTimer-= Time.deltaTime;
        }
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
        if (_isDodging || _dodgeCooldownTimer > 0) return;

        if(_velocity.sqrMagnitude > 0.01f)
        {
            _isDodging = true;
            _dodgeTime = _dodgeDuration;
            _dodgeDirection = new Vector3(_velocity.x, 0, _velocity.z).normalized;
            _dodgeCooldownTimer = _dodgeCooldown;
        }
    }
}
