using System;
using UnityEngine;

public class KeyPickup : BasePickup
{
    public static event Action<int> OnPickup;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPickup?.Invoke(1);
            Destroy(gameObject);
        }
    }

    protected override void Update()
    {
       //no despawn
    }
}
