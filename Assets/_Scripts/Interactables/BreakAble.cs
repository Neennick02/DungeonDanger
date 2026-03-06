using System.Collections.Generic;
using UnityEngine;

public class BreakAble : BaseHealth
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            DrainHealth(1);
        }
    }
}
