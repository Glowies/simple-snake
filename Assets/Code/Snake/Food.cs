using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Food : MonoBehaviour
{
    [Inject]
    private readonly IGridService _gridService;
    private bool _eatLock = false;

    private void OnTriggerEnter(Collider other)
    {
        Snake snake;
        if (!other.TryGetComponent(out snake) || _eatLock)
        {
            return;
        }

        _eatLock = true;
        snake.EatFood();
        MoveToRandomCell();
        StartCoroutine(UnlockNextFrame());
    }

    private void MoveToRandomCell()
    {
        var newPos = _gridService.GetRandomCellPosition();
        newPos.y = transform.localPosition.y;
        transform.localPosition = newPos;
    }

    IEnumerator UnlockNextFrame()
    {
        yield return new WaitForEndOfFrame();
        _eatLock = false;
    }
}
