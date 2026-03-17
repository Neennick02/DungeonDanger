using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;

    private void Awake()
    {   //only enable continue button if data is saved
        if (PlayerPrefs.HasKey("xPos"))
        {
            continueButton.SetActive(true);
        }
    }

    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        LoadMainScene();
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("LevelBuildingScene");
    }
    public void Quit()
    {
        Application.Quit();
    }

}
