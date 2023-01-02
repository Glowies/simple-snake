using System;
using UnityEngine;

public enum ActionMap
{
    Player,
    UI
}

public interface IInputManager
{
    ActionMap CurrentActionMap {get; set;}
}

