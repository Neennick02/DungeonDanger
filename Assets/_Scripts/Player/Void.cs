using UnityEngine;

public class Void : MonoBehaviour
{
    [SerializeField] private Transform newPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
         
            CharacterController cc = other.GetComponent<CharacterController>();

            cc.enabled = false;
            
            other.gameObject.transform.position = newPosition.position;

            cc.enabled = true;
        }
    }
}
