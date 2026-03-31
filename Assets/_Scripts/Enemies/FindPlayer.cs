using FirstGearGames.Utilities.Objects;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    private bool asssigned = false;
    private void OnTriggerEnter(Collider other)
    {
        //if already assigned return
        if (asssigned) return;

        //check for player
        if (other.gameObject.CompareTag("Player"))
        {
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
