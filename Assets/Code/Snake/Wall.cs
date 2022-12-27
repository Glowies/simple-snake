using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Snake snake;
        if(!other.TryGetComponent(out snake))
        {
            return;
        }

        snake.Kill();
    }
}
