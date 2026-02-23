using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Collections;

public class OrderedTorchManager : MonoBehaviour
{
    public List<int> CorrectOrder;
    public List<OrderedOnOffInteractable> Torches;
    public UnityEvent OnPuzzleSolved;

    public List<int> currentOrder = new List<int>();

    private void Start()
    {
        foreach(var torch in Torches)
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
        foreach (var torch in Torches)
        {
            torch.KeepOn();

        }
    }

    IEnumerator ResetPuzzle()
    {
        yield return new WaitForSeconds(1);
        currentOrder.Clear();

        foreach(var torch in Torches)
        {
            torch.TurnOff();
            
        }
    }
}
