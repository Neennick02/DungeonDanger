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
        //loop over all objects
        foreach(var Torch in Torches)
        {
            //if 1 or more are disabled return
            if (!Torch.isLit)
                return;

            //keep on if all are enabled
            Torch.KeepOn();
        }

        //invoke event
        OnAllTorchesLit?.Invoke();
    }
}
