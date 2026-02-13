using System;
using UnityEngine;

namespace NewInputByReference.Examples
{
    public class MenuManager : MonoBehaviour
    {
        public static event Action<bool> OnMenuUpdate;
         
        [Header("Settings")]
        [SerializeField] private string menuActionName;
        [Space(10)]
        [SerializeField] private string playerActionMapName;
        [SerializeField] private string menuActionMapName;

        private Transform _transform;
        
        private string _currentMenuActionName;
        private bool _isOpened;

        private string MenuActionName => ActionMap + '/' + menuActionName;
        private string ActionMap => _isOpened ? menuActionMapName : playerActionMapName;

        private void Awake()
        {
            _transform = transform;
            UpdateMenu();
        }
        
        private void Update()
        {
            if (!NewInput.GetButtonDown(_currentMenuActionName)) 
                return; 
            
            _isOpened = !_isOpened;
            
            OnMenuUpdate?.Invoke(_isOpened);
            NewInput.SwitchActionMap(ActionMap);
            
            UpdateMenu();
        }

        // Default Button -> Button Component -> On Click() 
        public void ResetRebinds()
        {
            NewInput.ResetRebinds();
            OnMenuUpdate?.Invoke(true);
        }

        private void UpdateMenu()
        {
            _currentMenuActionName = MenuActionName;
            
            foreach (Transform child in _transform)
                child.gameObject.SetActive(_isOpened);
        }
    }
}