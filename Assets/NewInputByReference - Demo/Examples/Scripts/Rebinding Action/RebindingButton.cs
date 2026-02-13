using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NewInputByReference.Examples
{
    public class RebindingButton : MonoBehaviour
    {
        private const string INPUT_DEVICE = "<Keyboard>/";
        private const string NULL_INPUT = "NULL";
        
        [Header("Settings")]
        [SerializeField] private string actionName;
        [SerializeField] private int bindingIndex;
        
        private string _bindingPath;
        private Text _textUI;

        private void Start()
        {
            _textUI = GetComponentInChildren<Text>();
            SetTextUI();
        }

        private void OnEnable() => MenuManager.OnMenuUpdate += ResetRebinds;
        private void OnDisable() => MenuManager.OnMenuUpdate -= ResetRebinds;

        // Key -> Button Component -> On Click() 
        public void StartRebinding() 
        {
            _textUI.enabled = false;
            StartCoroutine(WaitForRebind());
        } 

        // Reset Button -> Button Component -> On Click() 
        public void ResetRebind()
        {
            NewInput.ResetRebind(actionName, bindingIndex);
            SetTextUI();
        }

        // Called when the rebinding is finished
        private void OnComplete()
        {
            SetTextUI();
            _textUI.enabled = true;
            _bindingPath = NULL_INPUT; 
        }
        
        // Called when the Default Button is pressed
        private void ResetRebinds(bool cache)
        {
            SetTextUI();
        }

        // Sets "textUI"'s text to the current binding name of the Input Action indicated by "inputData" (e.g. E, Tab, A, W, ...)
        private void SetTextUI()
        {
            _textUI.text = NewInput.GetBindingName(actionName, bindingIndex);
        }
        
        // Rebinds the input action indicated by "actionName" to the key got from "OnGUI"
        private IEnumerator WaitForRebind()
        {
            yield return new WaitUntil(() => _bindingPath != NULL_INPUT);

            NewInput.Rebind(actionName, _bindingPath, bindingIndex);
            OnComplete();
        }

        // Gets the pressed key when the Menu Panel is active
        private void OnGUI()
        {
            if (!gameObject.activeInHierarchy)
                return;
            
            var input = Event.current;
            if (!input.isKey || input.keyCode == KeyCode.None)
            {
                _bindingPath = NULL_INPUT;
                return;
            }

            _bindingPath = INPUT_DEVICE + input.keyCode;
        }
    }
}