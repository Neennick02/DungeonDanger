using System;
using UnityEngine;

public class SceneStartManager : MonoBehaviour
{

    public static event Action OnSceneLoad;
    private void Start()
    {
        if (PlayerPrefs.HasKey("xPos"))
        {
            Debug.Log("Save data loaded");
            OnSceneLoad?.Invoke();
        }
    }
}
