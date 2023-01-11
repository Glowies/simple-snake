using System;
using UnityEngine;

public interface IGameManager
{
    bool IsRunning {get;}
    void StartGame();
    void PauseGame();
    void UnpauseGame();
    void EndGame();
    void ShowEndScreen();
}

