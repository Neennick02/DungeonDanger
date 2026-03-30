using System;
using UnityEngine;

public class SceneStartManager : MonoBehaviour
{

    public static event Action OnSceneLoad;
    private void Start()
    {
        if (PlayerPrefs.HasKey("xPos"))
        {
            OnSceneLoad?.Invoke();
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
