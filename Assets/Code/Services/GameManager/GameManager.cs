using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class GameManager : MonoBehaviour, IGameManager
{
    public Snake SnakeHead;
    public Food FoodSpawner;
    public MenuController InGameMenu;
    public MenuController PauseMenu;
    public MenuController EndMenu;
    public MenuController PreEndMenu;

    [Zenject.Inject]
    private IHighScoreService _highScoreService;

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
        EnhancedTouchSupport.Enable();
        IsRunning = false;
    }

    private void Start()
    {
        ToggleGameEntities(false);
    }

    public void StartGame()
    {
        ToggleGameEntities(true);
        _scoreService.Reset();
        SnakeHead.Reset();
        SnakeHead.StartMoving();
        FoodSpawner.SpawnRandom();
        _menuManager.OpenMenu(InGameMenu);
        IsRunning = true;
    }

    public void PauseGame()
    {
        ToggleGameEntities(false);
        SnakeHead.StopMoving();
        _menuManager.OpenMenu(PauseMenu);
        IsRunning = false;
    }

    public void UnpauseGame()
    {
        ToggleGameEntities(true);
        SnakeHead.StartMoving();
        _menuManager.OpenMenu(InGameMenu);
        IsRunning = true;
    }

    public void EndGame()
    {
        SnakeHead.Kill();
        _highScoreService.RecordCurrentScore();
        _menuManager.OpenMenu(PreEndMenu);
        IsRunning = false;
    }

    public void ShowEndScreen()
    {
        ToggleGameEntities(false);
        _menuManager.OpenMenu(EndMenu);
        IsRunning = false;
    }

    private void ToggleGameEntities(bool active)
    {
        SnakeHead.ToggleEnabled(active);
        FoodSpawner.gameObject.SetActive(active);
    }
}