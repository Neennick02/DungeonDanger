using System;
using UnityEngine;

public class CoinPickup : BasePickup
{
    public static event Action<int> OnPickup;
    [SerializeField] private int amount;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPickup?.Invoke(amount);
            DestroySelf();
        }
    }
}
