using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneStartManager : MonoBehaviour
{
    [SerializeField] private Image blackPanel;
    [SerializeField] private float duration = 1f;
    public static event Action OnSceneLoad;
    private void Start()
    {
        blackPanel.color = Color.black;
        //fade black screen to clear

        if (PlayerPrefs.HasKey("xPos"))
        {
            OnSceneLoad?.Invoke();
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    IEnumerator FadeIn()
    {
        Debug.Log("fade");
        float timer = 0f;
        Color startColor = Color.black;
        Color endColor = Color.clear;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            blackPanel.color = Color.Lerp(startColor, endColor, timer / duration);
            yield return null;
        }

        blackPanel.color = endColor;
    }
}
