using System;
using UnityEngine;

public class HeartPickup : BasePickup
{
    [SerializeField] private int healthAmount = 5;
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
