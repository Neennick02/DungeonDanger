using System;
using UnityEngine;

public class PotionPickup : BasePickup
{
    public static event Action<int> OnPickup;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPickup?.Invoke(3);
            DestroySelf();
        }
    }
}
