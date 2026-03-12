using UnityEngine;
using UnityEngine.Events;

public abstract class BasePickup : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other)
    {

        //add code in children
    }

    public void DestroySelf()
    {
        Destroy(gameObject); 
    }
}
