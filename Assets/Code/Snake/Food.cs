using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Food : MonoBehaviour
{
    [Inject]
    private IGridService _gridService;

    private void OnTriggerEnter(Collider other)
    {
        Snake snake;
        if (!other.TryGetComponent(out snake))
        {
            return;
        }

        snake.EatFood();
        MoveToRandomCell();
    }

    private void MoveToRandomCell()
    {
        var newPos = _gridService.GetRandomCellPosition();
        newPos.y = transform.localPosition.y;
        transform.localPosition = newPos;
    }
}
