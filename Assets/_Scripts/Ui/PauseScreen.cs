using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    private bool isPaused;

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
}
