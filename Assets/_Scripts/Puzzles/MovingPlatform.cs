using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector3 _lastPosition;
    private PlayerMovement movement;

    public Vector3 Delta { get; private set; }

    void Start()
    {
        _lastPosition = transform.position;
    }

    void LateUpdate()
    {
        Delta = transform.position - _lastPosition;
        _lastPosition = transform.position;
    }
}
