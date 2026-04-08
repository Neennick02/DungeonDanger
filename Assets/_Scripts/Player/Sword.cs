using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    [SerializeField] private float delay = 1f;

    private float timer;
    private bool hit = false;

    [SerializeField] private List<AudioClip> swordHitSounds;
    [SerializeField] private GameObject hitParticle;
    private void Update()
    {

        timer += Time.deltaTime;

            if (timer >= delay){ 
                hit = false;
            }
    }
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(hitParticle, transform.position, Quaternion.identity);

        if (other.gameObject.CompareTag("Boss"))
        {
            Debug.Log("Hit boss");
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {

            var enemyHealth = other.gameObject.GetComponent<BaseHealth>();
            if (enemyHealth == null) enemyHealth = other.gameObject.GetComponentInParent<BaseHealth>();

            if (enemyHealth != null && !hit)
            {

                enemyHealth.DrainHealth(damage);
            }
        }
        else
        {
            if (!hit)
            {
                //hit  sound
                AudioManager.Instance.PlayClip(swordHitSounds);
            }
        }

        timer = 0f;
        hit = true;
    }
}
