using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController _characterController { get; set; }

    [Header("Movement")]
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _movementAcceleration = 10f;
    private Vector3 _velocity;
    public float _verticalVelocity { get; private set; }
    private TargetFinder targetFinder;
    public Vector3 move{get; private set;}
    public MovingPlatform currentPlatform = null;

    [Header("Jump")]
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpForce = 10;
    private bool _isJumping;
    private bool _wasGrounded;
    private float _startJumpHeight;

    [Header("Dodge")]
    [SerializeField] private float _dodgeForce = 10;
    [SerializeField] private float _dodgeDuration = 0.2f;
    [SerializeField] private float _dodgeCooldown = 1f;
    private Vector3 _dodgeDirection;
    private float _dodgeTime;
    private float _dodgeCooldownTimer;
    private bool _isDodging;
    
    public bool _isTargeting { get; private set; }

    [Header("Pushing Blocks Config")]
    public static GameObject pushAbleBlockObject;
    [SerializeField] private float pushOffset = 2;
    [SerializeField] private float pushDuration = 1.5f;

    [SerializeField] private PlayerAnimator animator;

    public enum PlayerState
    {
        Locomotion,
        Pushing,
        Dodging,
        Attacking,
        Cutscene
    }

    public PlayerState State;

    private void OnEnable()
    {
        PlayerInputHandler.OnMove += Move;
        PlayerInputHandler.OnDodge += Dodge;

        CustomCamera.OnCutSceneStart += WatchCutScene;
        CustomCamera.OnCutSceneEnd += EndCutScene;
    }

    private void OnDisable()
    {
        PlayerInputHandler.OnMove -= Move;
        PlayerInputHandler.OnDodge -= Dodge;

        CustomCamera.OnCutSceneStart -= WatchCutScene;
        CustomCamera.OnCutSceneEnd -= EndCutScene;
    }
    private void Awake()
    {
        targetFinder = GetComponent<TargetFinder>();
        State = PlayerState.Locomotion;
        _characterController = GetComponent<CharacterController>();
    }

    private void Move(Vector2 input)
    {
        switch (State) 
        {
            case PlayerState.Locomotion:
                HandleLocomotionMovement(input);
                break;
            case PlayerState.Dodging:
                HandleDodge(move, input);
                //only dash movement during dodging
                break;
            case PlayerState.Attacking:
                HandleGravity();

                break;
            case PlayerState.Pushing:
                //no movement during pushing
                break;
        }
    }

    private void HandleLocomotionMovement(Vector2 input)
    {
        //get camera position
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        //convert to 3d space
        move = camForward * input.y + camRight * input.x;

        //update dodge direction
        _dodgeDirection = move.normalized;

        RotateCharacter(move);

        _velocity = Vector3.MoveTowards(
        _velocity, move, _movementAcceleration * Time.deltaTime);

        HandleGravity();

        Vector3 finalMovement = _velocity * _movementSpeed;


        finalMovement.y = _verticalVelocity;

        if (!currentPlatform)
        {
            _characterController.Move(finalMovement * _movementSpeed * Time.deltaTime);
        }
        if(currentPlatform)
        {
            //when standing on a platform move along with it
            _characterController.Move(currentPlatform.Delta + finalMovement * _movementSpeed * Time.deltaTime);
        }

        //set animation movement speed
        animator.SetSpeed(input.magnitude, input.normalized.x, input.normalized.y);


    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //handle moving platforms
        if (hit.collider.TryGetComponent(out MovingPlatform platform))
        {
            currentPlatform = platform;
        }
        else
        {
            currentPlatform = null;
        }

        //handle rigidbody collisions
        Rigidbody rb = hit.collider.attachedRigidbody;

        if (rb != null)
        {
            Vector3 dir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

            rb.angularVelocity = dir * 5f;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out MovingPlatform platform))
        {
            currentPlatform = null;
        }
    }

    private void HandleGravity()
    {
        if (_characterController.isGrounded && _verticalVelocity < 0)
        {
            //less gravity when grounded
            _verticalVelocity = -2;
        }
        else
        {
            //normal amount
            _verticalVelocity += _gravity * Time.deltaTime;
        }
    }
    public void RotateCharacter(Vector3 move)
    {
        if (_isTargeting)
        {
            if (targetFinder != null && targetFinder.currentTarget != null)
            {

                Vector3 directionToTarget = targetFinder.currentTarget.transform.position - transform.position;
                directionToTarget.y = 0;

                //lookat target when selecting
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

                transform.rotation = targetRotation;
                transform.rotation = Quaternion.Slerp(
                transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            //rotate character in move direction
            if (move != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(move);

                transform.rotation = Quaternion.Slerp(
                transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void Update()
    {
        //update jump animation
        if (_verticalVelocity > 0.1)
        {
            animator.IsGrounded(false);
        }
        else if(_characterController.isGrounded && !_wasGrounded)
        {
            animator.IsGrounded(true);
        }

        CheckForLedges();

        if(_dodgeCooldownTimer > 0)
        {
            _dodgeCooldownTimer-= Time.deltaTime;
        }

        bool hasTarget = targetFinder.currentTarget != null;
        _isTargeting = hasTarget;
        animator.IsTargeting(hasTarget);
    }

    private void CheckForLedges()
    {
        bool grounded = _characterController.isGrounded;

        if (_wasGrounded && !grounded)
        {
            if (!_isJumping && _velocity.sqrMagnitude > 0.5f && IsRealLedge())
            {
                Jump();
               // Debug.Log("Jump");
            }
            else
            {
            }
        }

        if (grounded)
        {
            _isJumping = false;
        }

        _wasGrounded = grounded;
    }

    private bool IsRealLedge()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            //if ground is close
            return false;
        }
        else
        {
            return true;
        }
    }
    private void Jump()
    {
        _startJumpHeight = transform.position.y;


        if (!_isJumping)
        {
            if (_isDodging)
            {
                _verticalVelocity = _jumpForce * 1.2f;
            }
            else
            {
                _verticalVelocity = _jumpForce;
            }
            _isJumping = true;
        }
    }

    private void Dodge()
    {
        if (_isDodging || _dodgeCooldownTimer > 0 || !_characterController.isGrounded) return;

        if (_velocity.sqrMagnitude > 0.01f)
            {
            //start dodge action
            State = PlayerState.Dodging;
            _isDodging = true;
                _dodgeTime = _dodgeDuration;
                _dodgeCooldownTimer = _dodgeCooldown;


            }
    }

    private void HandleDodge(Vector3 move, Vector2 input)
    {

        if (_isDodging)
        {
            _dodgeTime -= Time.deltaTime;

            if (_isTargeting)  //dodge during targeting           
            {
                _verticalVelocity = _jumpForce / 2;

                if (input.y < -0.5f)
                {
                    StartCoroutine(BackFlip());
                }
                else if (input.x > 0.5f)
                {
                    Debug.Log("Hop right");
                    _dodgeDirection = transform.right;
                    animator.Hop();
                }
                else if (input.x < -0.5f)
                {
                    Debug.Log("Hop left");
                    _dodgeDirection = -transform.right;
                    animator.Hop();
                }
                else
                {
                    //normal roll forward
                    _verticalVelocity = 0;
                    _dodgeDirection = transform.forward;
                    animator.Roll();
               }
            }
            else
            {
                animator.Roll();
            }
            if (_dodgeTime > 0)
                {
                    _velocity = _dodgeDirection * _dodgeForce;
                }
                else
                {
                    _isDodging = false;
                    State = PlayerState.Locomotion;
            }

        }
    }
    public void PushBlock()
    {
        State = PlayerState.Pushing;
    }
    public void StopPushBlock()
    {
        State = PlayerState.Locomotion;
    }

    IEnumerator BackFlip()
    {
        animator.Flip();
        yield return new WaitForSeconds(0.1f);
        _dodgeDirection = -transform.forward;
        
    }

    private void ChangeState(PlayerState state)
    {
        State = state;
    }

    private PlayerState previousState;
    private void WatchCutScene()
    {
        previousState = State;
        ChangeState(PlayerState.Cutscene);
    }
    private void EndCutScene()
    {
        ChangeState(previousState);
    }
}
