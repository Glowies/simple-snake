using System;
using UnityEngine;

public interface IGameManager
{
    void StartGame();
    void PauseGame();
    void UnpauseGame();
    void EndGame();
}

