using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{

    public UnityEvent OpenDoor;
    private List<BaseEnemy> enemies;

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
        if(enemies.Count == 0)
        {
            OpenDoor?.Invoke();
        }
    }
}
