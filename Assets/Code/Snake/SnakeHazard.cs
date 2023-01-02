using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHazard : MonoBehaviour
{
    [Zenject.Inject]
    private IGameManager _gameManager;

    private void OnTriggerEnter(Collider other)
    {
        Snake snake;
        if(!other.TryGetComponent(out snake))
        {
            return;
        }

        _gameManager.EndGame();
    }
}
