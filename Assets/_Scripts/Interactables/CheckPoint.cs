using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Void voidScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            voidScript.checkPoint = true;
        }
    }
}
