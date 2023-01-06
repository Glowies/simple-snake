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
    public MenuController PreEndMenu;

    [Zenject.Inject]
    private IScoreService _scoreService;

    [Zenject.Inject]
    private IMenuManager _menuManager;

    public bool IsRunning 
    {
        get;
        private set;
    }

    private void Awake()
    {
        IsRunning = false;
        SetActiveGameEntities(false);
    }

    public void StartGame()
    {
        SetActiveGameEntities(true);
        _scoreService.Reset();
        SnakeHead.Reset();
        SnakeHead.StartMoving();
        _menuManager.OpenMenu(InGameMenu);
        IsRunning = true;
    }

    public void PauseGame()
    {
        SetActiveGameEntities(false);
        SnakeHead.StopMoving();
        _menuManager.OpenMenu(PauseMenu);
        IsRunning = false;
    }

    public void UnpauseGame()
    {
        SetActiveGameEntities(true);
        SnakeHead.StartMoving();
        _menuManager.OpenMenu(InGameMenu);
        IsRunning = true;
    }

    public void EndGame()
    {
        SnakeHead.Kill();
        _menuManager.OpenMenu(PreEndMenu);
        IsRunning = false;
    }

    public void ShowEndScreen()
    {
        SetActiveGameEntities(false);
        _menuManager.OpenMenu(EndMenu);
        IsRunning = false;
    }

    private void SetActiveGameEntities(bool active)
    {
        SnakeHead.gameObject.SetActive(active);
        FoodSpawner.SetActive(active);
    }
}