using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour, IMenuManager
{
    public MenuController MainMenu;
    public CameraAnimator CameraAnimator;

    private MenuController[] _menus;
    private MenuController _openMenu;

    private void Awake()
    {
        _menus = GetComponentsInChildren<MenuController>(true);
    }

    private void Start()
    {
        DisableAllMenus();
        OpenMainMenu();
    }

    public void OpenMainMenu() => OpenMenu(MainMenu);
    
    public void OnNavigate(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();

        if(direction.x > .5)
        {
            _openMenu.RightPage();
        }
        else if(direction.x < -.5)
        {
            _openMenu.LeftPage();
        }
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        if(!context.started)
        {
            return;
        }

        _openMenu.TriggerActiveFace();
    }

    public void OpenMenu(MenuController menu)
    {
        if(_openMenu != null)
        {
            _openMenu.gameObject.SetActive(false);
        }
        
        _openMenu = menu;
        _openMenu.Open();

        if(_openMenu.ZoomedIn)
        {
            CameraAnimator.ZoomIn();
        }
        else
        {
            CameraAnimator.ZoomOut();
        }
    }

    private void DisableAllMenus()
    {
        foreach(var menu in _menus)
        {
            menu.gameObject.SetActive(false);
        }
    }

    public void RightPage() => _openMenu?.RightPage();

    public void LeftPage() => _openMenu?.LeftPage();
}

