using UnityEngine;

public class Void : MonoBehaviour
{
    [SerializeField] private Transform newPosition;
    [SerializeField] private Transform checkPointPos;
    public bool checkPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
         
            CharacterController cc = other.GetComponent<CharacterController>();

            cc.enabled = false;
            
            if(!checkPoint)
                other.gameObject.transform.position = newPosition.position;
            if(checkPoint)
                other.gameObject.transform.position = checkPointPos.position;


            cc.enabled = true;
        }
    }

}
