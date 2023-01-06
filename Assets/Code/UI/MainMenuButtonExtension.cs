using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Button3D))]
public class MainMenuButtonExtension : MonoBehaviour
{
    [Zenject.Inject]
    private readonly IMenuManager _menuManager;
    private Button3D _button;

    private void Awake()
    {
        TryGetComponent(out _button);
        _button.OnTouchedUI.AddListener(OpenTargetMenu);
    }

    public void OpenTargetMenu()
    {
        _menuManager.OpenMainMenu();
    }
}
