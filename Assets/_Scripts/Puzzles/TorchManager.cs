using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TorchManager : MonoBehaviour
{
    public List<OnOffInteractable> Torches;
    public UnityEvent OnAllTorchesLit;

    private void Update()
    {
        foreach(var Torch in Torches)
        {
            if (!Torch.isLit)
                return;

            Torch.KeepOn();
        }
        OnAllTorchesLit.Invoke();
       
    }
}
