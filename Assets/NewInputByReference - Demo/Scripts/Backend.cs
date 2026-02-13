using UnityEngine;
using UnityEngine.InputSystem;

namespace NewInputByReference.BackEnd
{
    internal static class Backend
    {
        private const string REBINDS_DIRECTORY = "NewInputByReference.Rebinds";

        private static PlayerInput _playerInput; 
 
        private static string RebindsDirectory => REBINDS_DIRECTORY + '.' + _playerInput.name;
        
        #region Utility Functions
         
        public static void SetPlayerInput(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            LoadRebinds();
        }
        
        public static InputAction GetAction(string actionName) => _playerInput.actions[actionName];
        
        #endregion

        #region Action Map Functions

        public static void SwitchActionMap(string actionMap)
        {
            _playerInput.SwitchCurrentActionMap(actionMap);
        }
        
        public static void EnableActionMap(string actionMap)
        {
            _playerInput.actions.FindActionMap(actionMap).Enable();
        }
        
        public static void DisableActionMap(string actionMap)
        {
            _playerInput.actions.FindActionMap(actionMap).Disable();
        }

        #endregion
        
        #region Rebinding Action Functions
        
        public static void Rebind(InputAction inputAction, string bindingPath, int bindingIndex)
        {
            inputAction.ApplyBindingOverride(bindingIndex, bindingPath);
            SaveRebinds();
        }
        
        public static string GetBindingName(InputAction inputAction, int bindingIndex)
        {
            string bindingName = InputControlPath.ToHumanReadableString(
                inputAction.bindings[bindingIndex].effectivePath, 
                InputControlPath.HumanReadableStringOptions.OmitDevice);

            return bindingName;
        }
        
        public static void ResetRebinds()
        {
            #if INPUT_SYSTEM_VERSION_111
                _playerInput.actions.RemoveAllBindingOverrides();
                
                PlayerPrefs.SetString(RebindsDirectory, string.Empty);
            #else
                Debug.LogWarning("NewInputByReference: <color=#FF0000>The Input System Package is outdated, " +
                                 "and it must be updated to version 1.1.1 or above in order to reset the rebinds.</color>");
            #endif
        }

        public static void ResetRebind(InputAction inputAction, int bindingIndex)
        {
            if (bindingIndex == 0) 
                inputAction.RemoveAllBindingOverrides(); 
            else 
                inputAction.RemoveBindingOverride(bindingIndex);
            
            SaveRebinds();
        }
        
        private static void SaveRebinds()
        {
            #if INPUT_SYSTEM_VERSION_111
                string rebinds = _playerInput.actions.SaveBindingOverridesAsJson();
                
                PlayerPrefs.SetString(RebindsDirectory, rebinds);
            #else
                Debug.LogWarning("NewInputByReference: <color=#FF0000>The Input System Package is outdated, " +
                                 "and it must be updated to version 1.1.1 or above in order to save/load the rebinds.</color>");
            #endif
        }

        private static void LoadRebinds()
        {
            #if INPUT_SYSTEM_VERSION_111
                string rebinds = PlayerPrefs.GetString(RebindsDirectory, string.Empty);

                if (string.IsNullOrEmpty(rebinds))
                    return;
            
                _playerInput.actions.LoadBindingOverridesFromJson(rebinds);
            #else
                Debug.LogWarning("NewInputByReference: <color=#FF0000>The Input System Package is outdated, " +
                                 "and it must be updated to version 1.1.1 or above in order to save/load the rebinds.</color>");
            #endif
        }
        
        #endregion

        #region Input Action Functions

        public static bool GetButtonDown(InputAction inputAction)
        {
            if (!CheckInputActionPhase(inputAction))
                return false;
            
            bool buttonClicked = inputAction.triggered && inputAction.ReadValue<float>() > 0;
            return buttonClicked; 
        }
        
        public static bool GetButton(InputAction inputAction)
        {
            return CheckInputActionPhase(inputAction);
        }
        
        public static bool GetButtonUp(InputAction inputAction)
        {
            bool buttonReleased = inputAction.triggered && inputAction.ReadValue<float>() <= 0;
            return buttonReleased; 
        }
        
        public static float GetAxis(InputAction inputAction)
        {
            return CheckInputActionPhase(inputAction) ? inputAction.ReadValue<float>() : 0f; 
        }

        public static Vector2 GetVector2(InputAction inputAction)
        {
            return CheckInputActionPhase(inputAction) ? inputAction.ReadValue<Vector2>() : Vector2.zero; 
        }
        
        public static Vector3 GetVector3(InputAction inputAction)
        {
            return CheckInputActionPhase(inputAction) ? inputAction.ReadValue<Vector3>() : Vector3.zero; 
        }

        #endregion
        
        #region  Check Function
        
        private static bool CheckInputActionPhase(InputAction inputAction)
        {
            var inputActionPhase = inputAction.phase;
            return inputActionPhase != InputActionPhase.Disabled && inputActionPhase != InputActionPhase.Waiting;
        }

        #endregion
        
    }
}