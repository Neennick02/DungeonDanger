using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TorchManager : MonoBehaviour
{
    public List<OnOffInteractable> Torches;
    public UnityEvent OnAllTorchesLit;
    [SerializeField] private List<AudioClip> doorSounds;
    [SerializeField] private List<AudioClip> clickSounds;
    [SerializeField] private List<AudioClip> solvedSounds;
    private bool opened;
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

        if (!opened)
        {
            AudioManager.Instance.PlayClip(doorSounds);
            AudioManager.Instance.PlayClip(clickSounds);
            AudioManager.Instance.PlayClip(solvedSounds);

            opened = true;
        }
    }
}
