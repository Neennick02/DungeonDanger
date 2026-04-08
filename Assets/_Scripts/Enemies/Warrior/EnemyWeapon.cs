using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private EnemyObject enemySO;
    [SerializeField] private List<AudioClip> impactSounds;
    [SerializeField] private List<AudioClip> playerImpactSounds;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();

            if (health != null)
            {
                AudioManager.Instance.PlayClip(playerImpactSounds);
                health.DrainHealth(enemySO.Damage);
            }
        }
        else
        {
            AudioManager.Instance.PlayClip(impactSounds);
        }
    } 
}
