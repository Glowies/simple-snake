using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    public Snake SnakeHead;
    public GameObject FoodSpawner;
    public MenuController InGameMenu;
    public MenuController PauseMenu;
    public MenuController EndMenu;

    [Zenject.Inject]
    private IInputManager _inputManager;

    [Zenject.Inject]
    private IScoreService _scoreService;

    [Zenject.Inject]
    private IMenuManager _menuManager;

    private void Awake()
    {
        SetActiveGameEntities(false);
    }

    public void StartGame()
    {
        SetActiveGameEntities(true);
        _scoreService.Reset();
        SnakeHead.Reset();
        SnakeHead.StartMoving();
        _menuManager.OpenMenu(InGameMenu);
        _inputManager.CurrentActionMap = ActionMap.Player;
    }

    public void PauseGame()
    {
        SetActiveGameEntities(false);
        SnakeHead.StopMoving();
        _inputManager.CurrentActionMap = ActionMap.UI;
        _menuManager.OpenMenu(PauseMenu);
    }

    public void UnpauseGame()
    {
        SetActiveGameEntities(true);
        SnakeHead.StartMoving();
        _inputManager.CurrentActionMap = ActionMap.Player;
        _menuManager.OpenMenu(InGameMenu);
    }

    public void EndGame()
    {
        SnakeHead.Kill();
        _inputManager.CurrentActionMap = ActionMap.UI;
        _menuManager.OpenMenu(EndMenu);
        SetActiveGameEntities(false);
    }

    private void SetActiveGameEntities(bool active)
    {
        SnakeHead.gameObject.SetActive(active);
        FoodSpawner.SetActive(active);
    }
}