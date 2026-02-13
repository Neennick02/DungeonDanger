using NewInputByReference.BackEnd;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NewInputByReference
{
    public static class NewInput
    { 
        public static void SetPlayerInput(PlayerInput playerInput) => Backend.SetPlayerInput(playerInput);

        #region Action Map Functions
        
        /// <summary>
        /// Change the current Action Map to the one indicated by "actionMap".
        /// </summary>
        /// <param name="actionMap">Can be found in the Input Action Asset, under the "Action Maps" column.</param>
        public static void SwitchActionMap(string actionMap)
        {
            Backend.SwitchActionMap(actionMap);
        }
        
        /// <summary>
        /// Enable the Action Map indicated by "actionMap". Other Action Maps will not be disabled if they are currently active".
        /// </summary>
        /// <param name="actionMap">Can be found in the Input Action Asset, under the "Action Maps" column.</param>
        public static void EnableActionMap(string actionMap)
        {
            Backend.EnableActionMap(actionMap);
        }
        
        /// <summary>
        /// Disable the Action Map indicated by "actionMap". Other Action Maps will not be disabled if they are currently active".
        /// </summary>
        /// <param name="actionMap">Can be found in the Input Action Asset, under the "Action Maps" column.</param>
        public static void DisableActionMap(string actionMap)
        {
            Backend.DisableActionMap(actionMap);
        }
        
        #endregion

        #region Rebinding Action Functions

        /// <summary>
        /// Rebind the input action indicated by "actionName".
        /// </summary>
        /// <param name="actionName">Can be found in the Input Action Asset, under the "Actions" column. <br /><br /></param>
        /// <param name="bindingPath">Can be determined in the Input Action Asset, under the "Actions" column -> (any) Action -> Path. (e.g. &lt;Keyboard&gt;/e, &lt;Gamepad&gt;/leftStick) <br /><br />
        /// Remark: To avoid GC, the passed variable should be cached into the class. <br /><br /></param>
        /// <param name="bindingIndex">Index of the input action, indicated by "actionName", binding that will be rebounded. <br /><br />
        /// Can be determined in the Input Action Asset, under the "Actions" column -> (any) Action -> Left DropDown Arrow. (check Documentation -> "6.What are some tips?", to understand how they are numbered)</param>
        public static void Rebind(string actionName, string bindingPath, int bindingIndex = 0)
        {
            var inputAction = Backend.GetAction(actionName);
            Backend.Rebind(inputAction, bindingPath, bindingIndex);
        }
        
        /// <summary>
        /// Get the name of the current binding indicated by "actionName". (e.g. W, E, Up Arrow, Tab, etc.)
        /// </summary>
        /// <param name="actionName">Can be found in the Input Action Asset, under the "Actions" column.</param>
        /// <param name="bindingIndex">Index of the input action, indicated by "actionName", binding that will provide the name. <br /><br />
        /// Can be determined in the Input Action Asset, under the "Actions" column -> (any) Action -> Left DropDown Arrow. (check Documentation -> "6.What are some tips?", to understand how they are numbered)</param>
        public static string GetBindingName(string actionName, int bindingIndex = 0)
        {
            var inputAction = Backend.GetAction(actionName);
            return Backend.GetBindingName(inputAction, bindingIndex);
        }
        
        /// <summary>
        /// Reset all rebinds of the current Input Action Asset, attached to the Player Input Component, to the default value.
        /// </summary>
        public static void ResetRebinds()
        {
            Backend.ResetRebinds();
        }
        
        /// <summary>
        /// Reset the rebinding of the input action indicated by "actionName".
        /// </summary>
        /// <param name="actionName">Can be found in the Input Action Asset, under the "Actions" column. <br /><br /></param>
        /// <param name="bindingIndex">Index of the input action, indicated by "actionName", binding that will be reset. <br /><br />
        /// If "bindingIndex" = 0, the function will reset all rebinds of the input action, indicated by "actionName". <br /><br />
        /// Can be determined in the Input Action Asset, under the "Actions" column -> (any) Action -> Left DropDown Arrow. (check Documentation -> "6.What are some tips?", to understand how they are numbered)</param>
        public static void ResetRebind(string actionName, int bindingIndex = 0)
        {
            var inputAction = Backend.GetAction(actionName);
            Backend.ResetRebind(inputAction, bindingIndex);
        }

        #endregion
        
        #region Action Name Functions

        /// <summary>
        /// Returns true if the user pressed the virtual Button indicated by "actionName" during the current frame.
        /// </summary>
        /// <param name="actionName">Can be found in the Input Action Asset, under the "Actions" column. (Action Type = Button)</param>
        public static bool GetButtonDown(string actionName)
        {
            var inputAction = Backend.GetAction(actionName);
            return Backend.GetButtonDown(inputAction);
        }
        
        /// <summary>
        /// Returns true if the user had pressed and did not release the virtual Button indicated by "actionName" during the current frame.
        /// </summary>
        /// <param name="actionName">Can be found in the Input Action Asset, under the "Actions" column. (Action Type = Button)</param>
        public static bool GetButton(string actionName)
        {
            var inputAction = Backend.GetAction(actionName);
            return Backend.GetButton(inputAction);
        }
        
        /// <summary>
        /// Returns true if the user released the virtual Button indicated by "actionName" during the current frame.
        /// </summary>
        /// <param name="actionName">Can be found in the Input Action Asset, under the "Actions" column. (Action Type = Button)</param>
        public static bool GetButtonUp(string actionName)
        {
            var inputAction = Backend.GetAction(actionName);
            return Backend.GetButtonUp(inputAction); 
        }
        
        /// <summary>
        /// Returns the value of the virtual Axis indicated by "actionName".
        /// </summary>
        /// <param name="actionName">Can be found in the Input Action Asset, under the "Actions" column. (Action Type = Value, Control Type = Axis)</param>
        public static float GetAxis(string actionName)
        {
            var inputAction = Backend.GetAction(actionName);
            return Backend.GetAxis(inputAction);
        }

        /// <summary>
        /// Returns the value of the virtual Vector2 indicated by "actionName".
        /// </summary>
        /// <param name="actionName">Can be found in the Input Action Asset, under the "Actions" column. (Action Type = Value, Control Type = Vector 2)</param>
        public static Vector2 GetVector2(string actionName)
        {
            var inputAction = Backend.GetAction(actionName);
            return Backend.GetVector2(inputAction);
        }
        
        /// <summary>
        /// Returns the value of the virtual Vector3 indicated by "actionName".
        /// </summary>
        /// <param name="actionName">Can be found in the Input Action Asset, under the "Actions" column. (Action Type = Value, Control Type = Vector 3)</param>
        public static Vector3 GetVector3(string actionName)
        {
            var inputAction = Backend.GetAction(actionName);
            return Backend.GetVector3(inputAction);
        }
        
        #endregion
        
    }
}