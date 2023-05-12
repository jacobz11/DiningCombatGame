using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.scrips
{
    internal class GameInput : NetworkBehaviour
    {
        //public static GameInput Instance { get; private set; }

        public event EventHandler OnStartChargingAction;
        public event EventHandler OnStopChargingAction;
        public event EventHandler OnPickUpAction;
        public event EventHandler OnJumpAction;
        public event EventHandler OnBostRunnigAction;

        //public event EventHandler OnPauseAction;
        //public event EventHandler OnBindingRebind;

        public enum Binding
        {
            Move_Up,
            Move_Down,
            Move_Left,
            Move_Right,
            Interact,
            InteractAlternate,
            Pause,
            Gamepad_Interact,
            Gamepad_InteractAlternate,
            Gamepad_Pause
        }

        private const string PLAYER_PREFS_BINDINGS = "InputBindings";

        private PlayerInput playerInput;
        private void Awake()
        {
            playerInput = new PlayerInput();
            playerInput.Player.Enable();

            playerInput.Player.Jump.performed += Jump_perfomed;
            playerInput.Player.PickUp.performed += PickUp_perfomed;
            playerInput.Player.StartCharging.performed += StartCharging_perfomed;
            playerInput.Player.StopChargung.performed += StopChargung_Performed;
            playerInput.Player.BostRunnig.performed += BostRunnig_perfomed;
        }

        private void StopChargung_Performed(InputAction.CallbackContext obj)
        {
            OnStopChargingAction?.Invoke(this, EventArgs.Empty);
        }

        public override void OnDestroy()
        {
            playerInput.Player.Jump.performed -= Jump_perfomed;
            playerInput.Player.PickUp.performed -= PickUp_perfomed;
            playerInput.Player.StartCharging.performed -= StartCharging_perfomed;
            playerInput.Player.StopChargung.performed -= StopChargung_Performed;
            playerInput.Player.BostRunnig.performed -= BostRunnig_perfomed;

            playerInput.Dispose();
            base.OnDestroy();
        }

        private void PickUp_perfomed(InputAction.CallbackContext obj)
        {
            OnPickUpAction?.Invoke(this, EventArgs.Empty);
        }

        private void Jump_perfomed(InputAction.CallbackContext obj)
        {
            OnJumpAction?.Invoke(this, EventArgs.Empty);
        }

        private void BostRunnig_perfomed(InputAction.CallbackContext obj)
        {
            OnBostRunnigAction?.Invoke(this, EventArgs.Empty);
        }

        private void StartCharging_perfomed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            OnStartChargingAction?.Invoke(this, EventArgs.Empty);
        }

        public Vector2 GetMovementVectorNormalized()
        {
            return playerInput.Player.Move.ReadValue<Vector2>().normalized;
        }

        public float GetRotin()
        {
            return playerInput.Player.Rotiatin.ReadValue<float>();
        }

        //public string GetBindingText(Binding binding)
        //{
        //    switch (binding)
        //    {
        //        default:
        //        case Binding.Move_Up:
        //            return playerInput.Player.Move.bindings[1].ToDisplayString();
        //        case Binding.Move_Down:
        //            return playerInput.Player.Move.bindings[2].ToDisplayString();
        //        case Binding.Move_Left:
        //            return playerInput.Player.Move.bindings[3].ToDisplayString();
        //        case Binding.Move_Right:
        //            return playerInput.Player.Move.bindings[4].ToDisplayString();
        //        case Binding.Interact:
        //            return playerInput.Player.Interact.bindings[0].ToDisplayString();
        //        case Binding.InteractAlternate:
        //            return playerInput.Player.InteractAlternate.bindings[0].ToDisplayString();
        //        case Binding.Pause:
        //            return playerInput.Player.Pause.bindings[0].ToDisplayString();
        //        case Binding.Gamepad_Interact:
        //            return playerInput.Player.Interact.bindings[1].ToDisplayString();
        //        case Binding.Gamepad_InteractAlternate:
        //            return playerInput.Player.InteractAlternate.bindings[1].ToDisplayString();
        //        case Binding.Gamepad_Pause:
        //            return playerInput.Player.Pause.bindings[1].ToDisplayString();
        //    }
        //}

        //public void RebindBinding(Binding binding, Action onActionRebound)
        //{
        //    playerInput.Player.Disable();

        //    InputAction inputAction;
        //    int bindingIndex;

        //    switch (binding)
        //    {
        //        default:
        //        case Binding.Move_Up:
        //            inputAction = playerInput.Player.Move;
        //            bindingIndex = 1;
        //            break;
        //        case Binding.Move_Down:
        //            inputAction = playerInput.Player.Move;
        //            bindingIndex = 2;
        //            break;
        //        case Binding.Move_Left:
        //            inputAction = playerInput.Player.Move;
        //            bindingIndex = 3;
        //            break;
        //        case Binding.Move_Right:
        //            inputAction = playerInput.Player.Move;
        //            bindingIndex = 4;
        //            break;
        //        case Binding.Interact:
        //            inputAction = playerInput.Player.Interact;
        //            bindingIndex = 0;
        //            break;
        //        case Binding.InteractAlternate:
        //            inputAction = playerInput.Player.InteractAlternate;
        //            bindingIndex = 0;
        //            break;
        //        case Binding.Pause:
        //            inputAction = playerInput.Player.Pause;
        //            bindingIndex = 0;
        //            break;
        //        case Binding.Gamepad_Interact:
        //            inputAction = playerInput.Player.Interact;
        //            bindingIndex = 1;
        //            break;
        //        case Binding.Gamepad_InteractAlternate:
        //            inputAction = playerInput.Player.InteractAlternate;
        //            bindingIndex = 1;
        //            break;
        //        case Binding.Gamepad_Pause:
        //            inputAction = playerInput.Player.Pause;
        //            bindingIndex = 1;
        //            break;
        //    }

        //    inputAction.PerformInteractiveRebinding(bindingIndex)
        //    .OnComplete(callback => {
        //        //Debug.Log(callback.action.bindings[1].path);
        //        //Debug.Log(callback.action.bindings[1].overridePath);
        //        callback.Dispose();
        //        playerInput.Player.Enable();
        //        onActionRebound();

        //        //playerInput.SaveBindingOverridesAsJson();
        //        PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInput.SaveBindingOverridesAsJson());
        //        PlayerPrefs.Save();

        //        OnBindingRebind?.Invoke(this, EventArgs.Empty);
        //    })
        //    .Start();
        //}
    }
}
