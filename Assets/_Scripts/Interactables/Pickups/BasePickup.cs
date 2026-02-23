using UnityEngine;
using UnityEngine.Events;

public abstract class BasePickup : MonoBehaviour
{
    public UnityEvent Pickup;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup?.Invoke();
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject); 
    }
}
