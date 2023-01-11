using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IMenuManager
{
    void OpenMenu(MenuController menu);
    void OpenMainMenu();
    void OnNavigate(InputAction.CallbackContext context);
    void OnSelect(InputAction.CallbackContext context);
    void RightPage();
    void LeftPage();
}

