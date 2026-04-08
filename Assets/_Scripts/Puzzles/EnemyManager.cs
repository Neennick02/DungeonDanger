using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    private bool locked = true;
    public UnityEvent OpenDoor;
    private List<BaseEnemy> enemies;
    [SerializeField] private GameObject key;
    [SerializeField] private List<AudioClip> doorSounds;
    [SerializeField] private List<AudioClip> solvedSounds;

    #region OnEnable
    private void OnEnable()
    {
        SaveStatueInteractable.OnSavePlayerData += SaveState;
        GameManager.OnLoad += LoadState;
    }

    private void OnDisable()
    {
        SaveStatueInteractable.OnSavePlayerData -= SaveState;
        GameManager.OnLoad -= LoadState;
    }
    #endregion
    private void Start()
    {
        enemies = new List<BaseEnemy>(GetComponentsInChildren<BaseEnemy>(true));

    }
    private void Update()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }
        if(enemies.Count == 0 && locked)
        {
            AudioManager.Instance.PlayClip(doorSounds);
            AudioManager.Instance.PlayClip(solvedSounds);
            OpenDoor?.Invoke();
            locked = false;
        }
    }

    private void SaveState()
    {
        int state = locked ? 1 : 0;
        PlayerPrefs.SetInt(transform.name + "doorState", state);
    }

    private void LoadState()
    {
        int state = PlayerPrefs.GetInt(transform.name + "doorState", 1);

        locked = state > 0 ? true : false;

        if (!locked)
        {
            OpenDoor?.Invoke();

            if(key != null)
            {
                key.SetActive(false);
            }
        }
    }
}
