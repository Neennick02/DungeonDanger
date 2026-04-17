using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject keyboard, controller;
    private bool isPaused;
    private bool isShowingControls;
    [SerializeField] private List<AudioClip> clickSounds;

    private void OnEnable()
    {
        PlayerInputHandler.OnPause += TogglePause;
    }
    private void OnDisable()
    {
        PlayerInputHandler.OnPause -= TogglePause;
    }
    public void TogglePause()
    {
        isPaused = !isPaused;

        holder.SetActive(isPaused);
        if (isPaused)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; 
        }
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

    public void Click()
    {
        AudioManager.Instance.PlayClip(clickSounds);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("LevelBuildingScene");
    }
}
