using System.Collections;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using static Unity.Cinemachine.IInputAxisOwner.AxisDescriptor;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;

    [Header("Movement")]
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _movementAcceleration = 10f;
    private Vector3 _velocity;
    private float _verticalVelocity;
    private TargetFinder targetFinder;

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
    
    
    //target
    private bool _isTargeting = false;

    [Header("Pushing Blocks Config")]
    public static GameObject pushAbleBlockObject;
    [SerializeField] private float pushOffset = 2;
    [SerializeField] private float pushDuration = 1.5f;
    private float pushTimer = 0f;
    private bool pushing = false;
    private Vector3 targetPos;
    [SerializeField] private LayerMask blockLayer;

    public enum PlayerState
    {
        Locomotion,
        Pushing,
        Dodging
    }

    public PlayerState State;

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
    private void Awake()
    {
        targetFinder = GetComponent<TargetFinder>();
        State = PlayerState.Locomotion;
    }

    private void Move(Vector2 input)
    {
        if (State == PlayerState.Locomotion ||
            State == PlayerState.Dodging)
        {
            HandleLocomotionMovement(input);
        }
        else if (State == PlayerState.Pushing)
        {
           // pushAbleBlockObject.GetComponent<>();
           
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
        Vector3 move = camForward * input.y + camRight * input.x;

        //update dodge direction
        _dodgeDirection = move.normalized;

        RotateCharacter(move);

        HandleDodge(move, input);

        HandleGravity();

        Vector3 finalMovement = _velocity * _movementSpeed;
        finalMovement.y = _verticalVelocity;

        _characterController.Move(finalMovement * _movementSpeed * Time.deltaTime);
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
/*
    [SerializeField] GameObject testcube;
    private void HandlePushingInput(Vector2 input)
    {
        HandleGravity();

       *//* //handle rotation 
        Quaternion targetRotation = Quaternion.LookRotation(pushAbleBlockObject.transform.position);

        transform.rotation = Quaternion.Slerp(
        transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);*//*


        //find the direction to push in

        Vector3 currentPos = pushAbleBlockObject.transform.position;

        // Direction from player to block
        Vector3 pushDirection = currentPos - transform.position;

        // Remove vertical influence
        pushDirection.y = 0f;

        // Decide dominant axis
        if (Mathf.Abs(pushDirection.x) > Mathf.Abs(pushDirection.z))
        {
            pushDirection = new Vector3(Mathf.Sign(pushDirection.x), 0f, 0f);
        }
        else
        {
            pushDirection = new Vector3(0f, 0f, Mathf.Sign(pushDirection.z));
        }

        targetPos = currentPos + pushDirection * pushOffset;

        if (!pushing)
            StartCoroutine(PushBlockRoutine(targetPos, pushDirection));
    }
    IEnumerator PushBlockRoutine(Vector3 targetPos, Vector3 pushDir)
    {
        {
            BoxCollider box = null;

            foreach (BoxCollider col in pushAbleBlockObject.GetComponents<BoxCollider>())
            {
                if (!col.isTrigger)
                {
                    box = col;
                    break;
                }
            }

            if (box == null)
                yield break;

            pushing = true;
            Vector3 startPos = pushAbleBlockObject.transform.position;

            // Make sure direction is flat & normalized
            Vector3 moveDir = pushDir;
            moveDir.y = 0f;
            moveDir = moveDir.normalized;

            while (Vector3.Distance(pushAbleBlockObject.transform.position, targetPos) > 0.05f)
            {
                pushTimer += Time.deltaTime;

                // Smooth movement
                Vector3 nextPos = Vector3.Lerp(
                    startPos,
                    targetPos,
                    pushTimer / pushDuration
                );

                // --- Collision Check Using BoxCast ---
                Vector3 worldCenter =
                    box.bounds.center;

                Vector3 halfExtents =
                    box.bounds.extents;

                bool blocked = Physics.BoxCast(
                    worldCenter,
                    halfExtents,
                    moveDir,
                    out RaycastHit hit,
                    Quaternion.identity,
                    pushOffset * 0.5f, // small safety buffer
                    blockLayer,
                    QueryTriggerInteraction.Ignore
                );

                if (blocked)
                {
                    Debug.Log("Blocked by: " + hit.collider.name);

                    pushTimer = 0f;
                    pushing = false;
                    yield break;
                }

                // Move if clear
                pushAbleBlockObject.transform.position = nextPos;

                yield return null;
            }

            // Snap to final position
            pushAbleBlockObject.transform.position = targetPos;

            pushTimer = 0f;
            pushing = false;

            pushAbleBlockObject.GetComponent<PushInteractable>()?.StopPushing();
            StopPushBlock();
            *//*        BoxCollider[] colliders = pushAbleBlockObject.GetComponents<BoxCollider>();

                    BoxCollider box = null;

                    Vector3 startPos = pushAbleBlockObject.transform.position;
                    foreach (BoxCollider col in colliders)
                    {
                        if (!col.isTrigger)
                        {
                            box = col;
                            break; // stop at the first non-trigger collider
                        }
                    }

                    PushInteractable pushScript = pushAbleBlockObject.GetComponent<PushInteractable>();
                    pushing = true;

                    while (Vector3.Distance(pushAbleBlockObject.transform.position, targetPos) > 0.1f)
                    {
                        pushTimer += Time.deltaTime;

                        Vector3 nextPos = Vector3.Lerp(
                            startPos,
                            targetPos,
                            pushTimer / pushDuration
                        );
                        *//*
                                    Vector3 halfExtents = box.size * 0.5f;
                                    Vector3 center = nextPos + box.center;
                                    Quaternion rotation = pushAbleBlockObject.transform.rotation;

                                    box.enabled = false;
                                    //Checks collision at NEXT position
                                  *//*  bool blocked = Physics.CheckBox(
                                        nextPos,                    
                                        halfExtents,
                                        rotation,
                                        blockLayer,
                                        QueryTriggerInteraction.Ignore
                                    );*//*


                                    Collider[] hits = Physics.OverlapBox(
                                        center,
                                        halfExtents,
                                        Quaternion.identity,
                                        ~0
                                    );


                                    Debug.Log("Hit count: " + hits.Length);
                                    testcube.transform.position = nextPos;

                                    box.enabled = true;*//*
                        Vector3 moveDir = pushDir;

                        box.enabled = false;

                        bool blocked = Physics.BoxCast(
                            box.bounds.center,
                            box.bounds.extents,
                            moveDir,
                            out RaycastHit hit,
                            Quaternion.identity,
                            pushOffset,
                            blockLayer,
                            QueryTriggerInteraction.Ignore
                        );

                        box.enabled = true;
                        if (blocked)
                        {
                            Debug.Log("Collision detected");

                            pushTimer = 0f;
                            transform.parent = null;

                            pushAbleBlockObject.GetComponent<PushInteractable>().StopPushing();
                            StopPushBlock();
                            pushing = false;

                            yield break;
                        }

                        // Only move if not blocked
                        pushAbleBlockObject.transform.position = nextPos;

                        yield return null;
                    }
                    //set position to target position
                    pushAbleBlockObject.transform.position = targetPos;

                    pushTimer = 0f;


                    //set isPushing to false
                    pushAbleBlockObject.GetComponent<PushInteractable>().StopPushing() ;
                    StopPushBlock();
                    pushing = false;*//*
        }
    }
*/
/*
    [SerializeField] GameObject testcube;
    private void HandlePushingInput(Vector2 input)
    {
        HandleGravity();

       *//* //handle rotation 
        Quaternion targetRotation = Quaternion.LookRotation(pushAbleBlockObject.transform.position);

        transform.rotation = Quaternion.Slerp(
        transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);*//*


        //find the direction to push in

        Vector3 currentPos = pushAbleBlockObject.transform.position;

        // Direction from player to block
        Vector3 pushDirection = currentPos - transform.position;

        // Remove vertical influence
        pushDirection.y = 0f;

        // Decide dominant axis
        if (Mathf.Abs(pushDirection.x) > Mathf.Abs(pushDirection.z))
        {
            pushDirection = new Vector3(Mathf.Sign(pushDirection.x), 0f, 0f);
        }
        else
        {
            pushDirection = new Vector3(0f, 0f, Mathf.Sign(pushDirection.z));
        }

        targetPos = currentPos + pushDirection * pushOffset;

        if (!pushing)
            StartCoroutine(PushBlockRoutine(targetPos, pushDirection));
    }
    IEnumerator PushBlockRoutine(Vector3 targetPos, Vector3 pushDir)
    {
        {
            BoxCollider box = null;

            foreach (BoxCollider col in pushAbleBlockObject.GetComponents<BoxCollider>())
            {
                if (!col.isTrigger)
                {
                    box = col;
                    break;
                }
            }

            if (box == null)
                yield break;

            pushing = true;
            Vector3 startPos = pushAbleBlockObject.transform.position;

            // Make sure direction is flat & normalized
            Vector3 moveDir = pushDir;
            moveDir.y = 0f;
            moveDir = moveDir.normalized;

            while (Vector3.Distance(pushAbleBlockObject.transform.position, targetPos) > 0.05f)
            {
                pushTimer += Time.deltaTime;

                // Smooth movement
                Vector3 nextPos = Vector3.Lerp(
                    startPos,
                    targetPos,
                    pushTimer / pushDuration
                );

                // --- Collision Check Using BoxCast ---
                Vector3 worldCenter =
                    box.bounds.center;

                Vector3 halfExtents =
                    box.bounds.extents;

                bool blocked = Physics.BoxCast(
                    worldCenter,
                    halfExtents,
                    moveDir,
                    out RaycastHit hit,
                    Quaternion.identity,
                    pushOffset * 0.5f, // small safety buffer
                    blockLayer,
                    QueryTriggerInteraction.Ignore
                );

                if (blocked)
                {
                    Debug.Log("Blocked by: " + hit.collider.name);

                    pushTimer = 0f;
                    pushing = false;
                    yield break;
                }

                // Move if clear
                pushAbleBlockObject.transform.position = nextPos;

                yield return null;
            }

            // Snap to final position
            pushAbleBlockObject.transform.position = targetPos;

            pushTimer = 0f;
            pushing = false;

            pushAbleBlockObject.GetComponent<PushInteractable>()?.StopPushing();
            StopPushBlock();
            *//*        BoxCollider[] colliders = pushAbleBlockObject.GetComponents<BoxCollider>();

                    BoxCollider box = null;

                    Vector3 startPos = pushAbleBlockObject.transform.position;
                    foreach (BoxCollider col in colliders)
                    {
                        if (!col.isTrigger)
                        {
                            box = col;
                            break; // stop at the first non-trigger collider
                        }
                    }

                    PushInteractable pushScript = pushAbleBlockObject.GetComponent<PushInteractable>();
                    pushing = true;

                    while (Vector3.Distance(pushAbleBlockObject.transform.position, targetPos) > 0.1f)
                    {
                        pushTimer += Time.deltaTime;

                        Vector3 nextPos = Vector3.Lerp(
                            startPos,
                            targetPos,
                            pushTimer / pushDuration
                        );
                        *//*
                                    Vector3 halfExtents = box.size * 0.5f;
                                    Vector3 center = nextPos + box.center;
                                    Quaternion rotation = pushAbleBlockObject.transform.rotation;

                                    box.enabled = false;
                                    //Checks collision at NEXT position
                                  *//*  bool blocked = Physics.CheckBox(
                                        nextPos,                    
                                        halfExtents,
                                        rotation,
                                        blockLayer,
                                        QueryTriggerInteraction.Ignore
                                    );*//*


                                    Collider[] hits = Physics.OverlapBox(
                                        center,
                                        halfExtents,
                                        Quaternion.identity,
                                        ~0
                                    );


                                    Debug.Log("Hit count: " + hits.Length);
                                    testcube.transform.position = nextPos;

                                    box.enabled = true;*//*
                        Vector3 moveDir = pushDir;

                        box.enabled = false;

                        bool blocked = Physics.BoxCast(
                            box.bounds.center,
                            box.bounds.extents,
                            moveDir,
                            out RaycastHit hit,
                            Quaternion.identity,
                            pushOffset,
                            blockLayer,
                            QueryTriggerInteraction.Ignore
                        );

                        box.enabled = true;
                        if (blocked)
                        {
                            Debug.Log("Collision detected");

                            pushTimer = 0f;
                            transform.parent = null;

                            pushAbleBlockObject.GetComponent<PushInteractable>().StopPushing();
                            StopPushBlock();
                            pushing = false;

                            yield break;
                        }

                        // Only move if not blocked
                        pushAbleBlockObject.transform.position = nextPos;

                        yield return null;
                    }
                    //set position to target position
                    pushAbleBlockObject.transform.position = targetPos;

                    pushTimer = 0f;


                    //set isPushing to false
                    pushAbleBlockObject.GetComponent<PushInteractable>().StopPushing() ;
                    StopPushBlock();
                    pushing = false;*//*
        }
    }
*/
    private void RotateCharacter(Vector3 move)
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
        CheckForLedges();

        _wasGrounded = _characterController.isGrounded;

        if(_dodgeCooldownTimer > 0)
        {
            _dodgeCooldownTimer-= Time.deltaTime;
        }


        if(targetFinder.currentTargetName != null)
        {
            _isTargeting = true;
        }
        else
        {
             _isTargeting= false;
        }
    }

    private void CheckForLedges()
    {
        bool grounded = _characterController.isGrounded;

        if(_wasGrounded && !grounded)
        {
            if (!_isJumping && _velocity.sqrMagnitude > 0.5f)
            {
                Jump();
            }
            else
            {
                Debug.Log("Hang");
            }
        }

        if (grounded) _isJumping = false;
    }

    private void Jump()
    {
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
        State = PlayerState.Dodging;
        if (_isDodging || _dodgeCooldownTimer > 0 || !_characterController.isGrounded) return;

        
            if (_velocity.sqrMagnitude > 0.01f)
            {
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

            if (_isTargeting)  //dodge during targeting              //assign animations later
            {
                _verticalVelocity = _jumpForce / 2;

                if (input.y < -0.5f)
                {
                    Debug.Log("Backflip");
                    _dodgeDirection = -transform.forward;

                   
                }
                else if (input.x > 0.5f)
                {
                    Debug.Log("Hop right");
                    _dodgeDirection = transform.right;

                }
                else if (input.x < -0.5f)
                {
                    Debug.Log("Hop left");
                    _dodgeDirection = -transform.right;

                }
                else
                {
                    Debug.Log("Jump forward");
                }


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
        else // normal movement
        {
            _velocity = Vector3.MoveTowards(
            _velocity, move, _movementAcceleration * Time.deltaTime);
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



    void OnDrawGizmos()
    {

    }
}
