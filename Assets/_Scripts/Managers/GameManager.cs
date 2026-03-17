using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GameStates
    {
        Tutorial,
        Playing,
        Paused,
        GameOver,
    }

    public GameStates State {get; private set;}
    public string CurrentStateName;

    private bool paused = false;

    //save / load
    public static event Action OnLoad;
    public static event Action OnSave;

    private void Awake()
    { 
        //setup signleton instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        PlayerInputHandler.OnPause += Pause;
        PlayerHealth.OnDeath += GameOver;
        SceneStartManager.OnSceneLoad += LoadPlayerData;
    }

    private void OnDisable()
    {
        PlayerInputHandler.OnPause -= Pause;
        PlayerHealth.OnDeath -= GameOver;
        SceneStartManager.OnSceneLoad -= LoadPlayerData;
    }

    private void Start()
    {
        State = GameStates.Playing;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Pause()
    {
        if (!paused)
        {
            State = GameStates.Paused;
            Cursor.visible = true;
        }
        else
        {
            State = GameStates.Playing;
            Cursor.visible = false;
        }

        paused = !paused;
    }

    private void GameOver()
    {
        State = GameStates.GameOver;
        Cursor.visible = true;
    }

    private void Update()
    {
        CurrentStateName = State.ToString();
    }

    public void LoadPlayerData()
    {
            OnLoad?.Invoke();        
    }
}
