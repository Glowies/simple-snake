using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager: MonoBehaviour, IInputManager
{
    private PlayerInput _playerInput;

    private void Awake()
    {
        TryGetComponent(out _playerInput);
    }

    public ActionMap CurrentActionMap 
    {
        get
        {
            var found = Enum.TryParse<ActionMap>(
                _playerInput.currentActionMap.name, 
                true, 
                out var result);
            
            return found ? result : ActionMap.UI;
        } 
        set
        {
            var actionMap = _playerInput.actions.FindActionMap(value.ToString(), true);
            _playerInput.currentActionMap = actionMap;
        }
    }

    [ContextMenu("player")]
    public void SetPlayer()
    {
        CurrentActionMap = ActionMap.Player;
    }
}

