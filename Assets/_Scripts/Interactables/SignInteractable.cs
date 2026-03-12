using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SignInteractable : BaseInteractable
{
    [SerializeField] private string Message;
    [SerializeField] private TextMeshProUGUI worldMessage;

    public static event Action<string> OpenSign;
    public static event Action CloseSign;
    private bool _enabled = false;
    protected override void Start()
    {
        base.Start();
        interactionPopup = "Read";
        worldMessage.text = Message;
    }

    public override void Interact()
    {
        if (!_enabled)
        {
            OpenSign?.Invoke(Message);
            _enabled = true;
        }
        else
        {
            CloseSign?.Invoke();
            _enabled = false;
        }
        
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        CloseSign?.Invoke();
        _enabled = false;
    }
}
