using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject keyboard, controller;
    private bool isPaused;
    private bool isShowingControls;
    private void OnEnable()
    {
        PlayerInput.OnPause += TogglePause;
    }
    private void OnDisable()
    {
        PlayerInput.OnPause -= TogglePause;
    }
    public void TogglePause()
    {
        isPaused = !isPaused;

        holder.SetActive(isPaused);
        if (isPaused) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void ShowControls()
    {
        isShowingControls = !isShowingControls;
        controls.SetActive(isShowingControls);

        if(isShowingControls ) {
            if (Gamepad.current != null)
            {
                controller.SetActive(true);
            }
            else
            {
                keyboard.SetActive(true);
            }
        }
        else
        {
            keyboard.SetActive(false);
            controller.SetActive(false);

        }
    }
}
