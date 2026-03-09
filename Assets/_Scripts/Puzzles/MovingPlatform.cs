using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private CharacterController cc;
    private Vector3 offset;

/*    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("test");

            cc = other.GetComponent<CharacterController>();
            offset = other.transform.position - transform.position;
            //cc.enabled = false;
            other.transform.SetParent(transform); 
        }
    }

    private void OnTriggerExit(Collider other)
    {

        other.transform.parent = null;
        cc = null;
    }

    private void LateUpdate()
    {
        if (cc != null)
        {
            cc.Move(transform.position + offset);
        }
    }*/

}
