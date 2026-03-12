using UnityEngine;
using UnityEngine.Events;

public abstract class BasePickup : MonoBehaviour
{
    [SerializeField] protected float despawnTime = 5;
    protected float timer;
    protected virtual void OnTriggerEnter(Collider other)
    {

        //add code in children
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;
        
        if(timer > despawnTime ) 
            DestroySelf();
    }

    public void DestroySelf()
    {
        Destroy(gameObject); 
    }
}
