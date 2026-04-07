using System;
using System.Collections;
using UnityEngine;

public class CoinPickup : BasePickup
{
    public static event Action<int> OnPickup;
    [SerializeField] private int amount;
    [SerializeField] private GameObject model;
    private bool pickedUp;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !pickedUp)
        {
            StartCoroutine(AddMoney(amount));
            model.SetActive(false);
            pickedUp = true;
        }
    }

    IEnumerator AddMoney(int amount)
    {
        int index = 0;
        float pitch = 1;

        while (index != amount)
        {
            index++;
            OnPickup?.Invoke(1);
            AudioManager.Instance.PlayClip(pickupSound, 1, pitch);
            pitch += 0.2f;
            yield return new WaitForSeconds(0.1f);
            
        }
        DestroySelf();
    }
}
