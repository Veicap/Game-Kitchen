using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static GameInput;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnToggleGamePause;
    public event EventHandler OnRibiBindingAction;
    private PlayerInputAction playerInputAction;
    private const string PLAYER_PREFS_BINDING = "playerPrefsBinding";
    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternate,
        Pause
    }
    private void Awake()
    {
        Instance = this;
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        playerInputAction.Player.Interact.performed += Interact_performed;
        playerInputAction.Player.InteracAlternate.performed += InteracAlternate_performed;
        playerInputAction.Player.Pause.performed += Pause_performed;
        if(PlayerPrefs.HasKey(PLAYER_PREFS_BINDING))
        {
            playerInputAction.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDING));
        }
    }
    private void OnDestroy()
    {
        playerInputAction.Player.Interact.performed -= Interact_performed;
        playerInputAction.Player.InteracAlternate.performed -= InteracAlternate_performed;
        playerInputAction.Player.Pause.performed -= Pause_performed;
        playerInputAction.Dispose();
    }
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnToggleGamePause?.Invoke(this, EventArgs.Empty);
    }

    private void InteracAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetInputVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.MoveUp:
                return playerInputAction.Player.Move.bindings[1].ToDisplayString();
            case Binding.MoveDown:
                return playerInputAction.Player.Move.bindings[2].ToDisplayString();
            case Binding.MoveLeft:
                return playerInputAction.Player.Move.bindings[3].ToDisplayString();
            case Binding.MoveRight:
                return playerInputAction.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerInputAction.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return playerInputAction.Player.InteracAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInputAction.Player.Pause.bindings[0].ToDisplayString();
        } 
    }
    public void RebiBinding(Binding binding, Action onActionRebound)
    {
        playerInputAction.Player.Disable();
        InputAction playerInputState;
        int indexBinding;
        switch (binding)
        {
            default:
            case Binding.MoveUp:
                playerInputState = playerInputAction.Player.Move;
                indexBinding = 1;
                break;
            case Binding.MoveDown:
                playerInputState = playerInputAction.Player.Move;
                indexBinding = 2;
                break;
            case Binding.MoveLeft:
                playerInputState = playerInputAction.Player.Move;
                indexBinding = 3;
                break;
            case Binding.MoveRight:
                playerInputState = playerInputAction.Player.Move;
                indexBinding = 4;
                break;
            case Binding.Interact:
                playerInputState = playerInputAction.Player.Interact;
                indexBinding = 0;
                break;
            case Binding.InteractAlternate:
                playerInputState = playerInputAction.Player.InteracAlternate;
                indexBinding = 0;
                break;
            case Binding.Pause:
                playerInputState = playerInputAction.Player.Pause;
                indexBinding = 0;
                break;
        }
        playerInputState.PerformInteractiveRebinding(indexBinding)
            .OnComplete(callback =>
            {
                playerInputAction.Player.Enable();
                onActionRebound();
                playerInputAction.SaveBindingOverridesAsJson();
                PlayerPrefs.SetString(PLAYER_PREFS_BINDING, playerInputAction.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
                OnRibiBindingAction?.Invoke(this, EventArgs.Empty); 
            }).Start();
    }
}
