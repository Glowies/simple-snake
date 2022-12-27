using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHazard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Snake snake;
        if(!other.TryGetComponent(out snake))
        {
            return;
        }
        print($"hello from {gameObject.name}");

        snake.Kill();
    }
}
