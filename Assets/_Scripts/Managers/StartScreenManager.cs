using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    public List<AudioClip> UI_Accept = new List<AudioClip>();
    public List<AudioClip> UI_Return = new List<AudioClip>();

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
        LoadMainScene();
    }

    public void LoadMainScene()
    {
        StartCoroutine(ClickRoutine(UI_Accept, "LevelBuildingScene"));
    }
    public void Quit()
    {
        StartCoroutine(ClickRoutine(UI_Return));
    }

    private IEnumerator ClickRoutine(List<AudioClip> clips, string sceneName = default)
    {
        AudioManager.Instance.PlayClip(clips);
        yield return new WaitForSeconds(0.3f);

        
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
