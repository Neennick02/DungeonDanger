using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class OrderedTorchManager : MonoBehaviour
{
    [SerializeField] private GameObject cutSceneCamera;
    public List<int> CorrectOrder;
    private List<OrderedOnOffInteractable> torches;
    public UnityEvent OnPuzzleSolved;

    private List<int> currentOrder = new List<int>();
    [SerializeField] private Animator _animator;
    private static bool locked = true;

    [SerializeField] private List<AudioClip> doorSounds;
    [SerializeField] private List<AudioClip> clickSounds;
    #region OnEnable
    private void OnEnable()
    {
        SaveStatueInteractable.OnSavePlayerData += SaveState;
        GameManager.OnLoad += LoadState;
    }

    private void OnDisable()
    {
        SaveStatueInteractable.OnSavePlayerData -= SaveState;
        GameManager.OnLoad -= LoadState;
    }
    #endregion
    private void Start()
    {
        //fill list with child objects
        torches = new List<OrderedOnOffInteractable>(GetComponentsInChildren<OrderedOnOffInteractable>(true));

        //subscribe to event
        foreach (var torch in torches)
        {
            torch.OnLit.AddListener(CheckSolution);            
        }
    }
    public void CheckSolution(int index)
    {
        currentOrder.Add(index);
        

        if(currentOrder.Count == CorrectOrder.Count)
        {
            if(currentOrder.SequenceEqual(CorrectOrder))
            {
                PuzzleSolved();
                locked = false;
            }
            else
            {
                StartCoroutine(ResetPuzzle());
            }
        }
    }

    private void PuzzleSolved()
    {
        OnPuzzleSolved?.Invoke();
        foreach (var torch in torches)
        {
            AudioManager.Instance.PlayClip(clickSounds);
            AudioManager.Instance.PlayClip(doorSounds);

            torch.KeepOn();

        }
    }

    IEnumerator ResetPuzzle()
    {
        yield return new WaitForSeconds(1);
        currentOrder.Clear();

        foreach(var torch in torches)
        {
            torch.TurnOff();
            
        }
    }
    public void EndAnimation()
    {
        StartCoroutine(ResetAnimator());
    }
    IEnumerator ResetAnimator()
    {
        yield return new WaitForSeconds(1.5f);
        _animator.enabled = false;
    }

    private void SaveState()
    {
        int state = locked ? 1 : 0;
        PlayerPrefs.SetInt(transform.name + "doorState", state);
    }

    private void LoadState()
    {
        int state = PlayerPrefs.GetInt(transform.name + "doorState", 1);

        locked = state > 0 ? true : false;

        if (!locked)
        {
            Destroy(cutSceneCamera);    
            OnPuzzleSolved?.Invoke();
        }
    }
}
