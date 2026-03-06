using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines.Interpolators;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    private float duration = 1;
    private float time = 0;
    private Image panel;
    private float alpha = 0;
    [SerializeField] private GameObject holder;

    private Color startC;
    private Color currentC;

    private void OnEnable()
    {
        PlayerHealth.OnDeath += EnableScreen;
    }
    private void OnDisable()
    {
        PlayerHealth.OnDeath -= EnableScreen;
    }
    private void Start()
    {
        panel = GetComponent<Image>();
        startC = panel.color;
    }


    public void EnableScreen()
    {
        StartCoroutine(OpenScreen());
    }

    public void Restart()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator OpenScreen()
    {
        alpha = 0;
        time = 0;

        while(time < duration)
        {
            time += Time.deltaTime;
            alpha = Mathf.Lerp(alpha, 1, time);
            currentC = new Color(startC.r, startC.g, startC.b , alpha);
            panel.color = currentC;

            yield return null;
        }
        holder.SetActive(true);
        Time.timeScale = 0;
    }
}
