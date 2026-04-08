using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    public List<AudioClip> clickSounds;

    private void Awake()
    {   //only enable continue button if data is saved
        if (PlayerPrefs.HasKey("xPos"))
        {
            continueButton.SetActive(true);
        }

        Time.timeScale = 1.0f;
    }

    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine(ClickRoutine("StoryScene"));
    }

    public void LoadMainScene()
    {
        StartCoroutine(ClickRoutine("LevelBuildingScene"));
    }
    public void Quit()
    {
        StartCoroutine(ClickRoutine());
    }

    private IEnumerator ClickRoutine(string sceneName = default)
    {
        AudioManager.Instance.PlayClip(clickSounds, 1f);
        yield return new WaitForSeconds(.3f);

        
        if(sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
        else //if there is no string than quit
        {
            Application.Quit();
        }
    }
}
