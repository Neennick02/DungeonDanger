using System.Collections.Generic;
using UnityEngine;

public class BreakAble : MonoBehaviour
{
    [SerializeField] private GameObject[] itemArray;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            Break();
        }
    }
    private void Break()
    {
        int randomInt = Random.Range(0, itemArray.Length);
        GameObject dropObject = itemArray[randomInt];

        Instantiate(dropObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
