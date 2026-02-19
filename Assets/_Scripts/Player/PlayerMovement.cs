using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _movementAcceleration = 10f;
    private Vector3 _velocity;

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
        Vector3 move = camForward * input.y + camRight * input.x;//new Vector3(input.x, 0, input.y);

        _velocity = Vector3.MoveTowards(
            _velocity, move, _movementAcceleration * Time.deltaTime);

        _characterController.Move(_velocity * _movementSpeed * Time.deltaTime);
    }

    private void Dodge()
    {

    }

}
