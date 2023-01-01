using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    public Snake SnakeHead;
    public MenuController Menu;
    public GameObject FoodSpawner;
    public GameObject ScoreDisplay;
    public GameObject PauseFace;

    [Zenject.Inject]
    private IInputManager _inputManager;

    private void Awake()
    {
        SetActiveGameEntities(false);
    }

    public void StartGame()
    {
        SetActiveGameEntities(true);
        SnakeHead.Reset();
        SnakeHead.StartMoving();
        Menu.HideAllFaces();
        _inputManager.CurrentActionMap = ActionMap.Player;
    }

    public void PauseGame()
    {
        SetActiveGameEntities(false);
        SnakeHead.StopMoving();
        Menu.ShowActiveFace();
        _inputManager.CurrentActionMap = ActionMap.UI;
        PauseFace.SetActive(true);
    }

    public void UnpauseGame()
    {
        SetActiveGameEntities(true);
        SnakeHead.StartMoving();
        Menu.HideAllFaces();
        _inputManager.CurrentActionMap = ActionMap.Player;
        PauseFace.SetActive(false);
    }

    public void EndGame()
    {
        SnakeHead.Kill();
        Menu.ShowActiveFace();
        _inputManager.CurrentActionMap = ActionMap.UI;
        SetActiveGameEntities(false);
    }

    private void SetActiveGameEntities(bool active)
    {
        SnakeHead.gameObject.SetActive(active);
        FoodSpawner.SetActive(active);
        ScoreDisplay.SetActive(active);
    }
}