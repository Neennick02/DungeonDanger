using UnityEngine;
using UnityEngine.InputSystem;

namespace NewInputByReference.Handler
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputHandler : MonoBehaviour
    {
        private void Awake() => NewInput.SetPlayerInput(GetComponent<PlayerInput>());
    }
}