using FirstGearGames.Utilities.Objects;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    private MusicManager musicManager;
    private bool asssigned = false;
    private bool listEmpty;

    private void Awake()
    {
        musicManager = FindFirstObjectByType<MusicManager>(); 
    }
    private void Update()
    {
        //remove empty from list
        enemies.RemoveAll(enemy =>  enemy == null);

        if (enemies.Count < 1 && !listEmpty)
        {
            listEmpty = true;
            musicManager.ToggleCombatMusic(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if already assigned return
        if (asssigned) return;

        //check for player
        if (other.gameObject.CompareTag("Player"))
        {
            musicManager.ToggleCombatMusic(true);

            for (int i = 0; i < enemies.Count; i++)
            {
                BaseEnemy script = enemies[i].GetComponent<BaseEnemy>();

                if (script == null) script = enemies[i].GetComponentInChildren<BaseEnemy>();

                //assign all enemy objects
                script.AssignPlayer(other.transform);
                asssigned = true;
            }
        }
    }
}
