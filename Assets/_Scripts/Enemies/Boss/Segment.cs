using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

public class Segment : MonoBehaviour
{
    [SerializeField] private BossObject bossObject;
    [SerializeField] private Collider trigger;
    [SerializeField] private BossBehaviour bossBehaviour;
    [SerializeField] private List<AudioClip> DamageClips;

    private static bool isHit;
    private float regenTime = 1f;
    private float timer;
    private Rigidbody rb;
    private float startHeight;
    public bool isTail;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startHeight = rb.position.y;
    }

    private void Update()
    {
        if (isHit)
        {
            timer += Time.deltaTime;

            if(timer > regenTime)
            {
                isHit = false;
                timer = 0;
            }
        }
    }
    public void UpdatePosition(Vector3 pos, Vector3 parentPos)
    {
        if (rb == null) return; 

        Vector3 newPos = new Vector3(pos.x, startHeight, pos.z);
        rb.MovePosition(newPos);

        Vector3 dir = parentPos - newPos;
        if (dir.sqrMagnitude > 0.0001f)
        {
            rb.MoveRotation(Quaternion.LookRotation(dir));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            health.DrainHealth(bossObject.Damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isHit) return;

        if (other.CompareTag("Sword") && isTail)
        {
            StartCoroutine(TakeDamageRoutine());
        }
    }

    public void EnableTrigger()
    {
        trigger.enabled = true;
        isTail = true;
    }

    IEnumerator TakeDamageRoutine()
    {
        AudioManager.Instance.PlayClip(DamageClips, 0.2f);
        isHit = true;
        bossBehaviour.AssignNewSegment();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
