using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Snake : MonoBehaviour
{
    public int Length = 1;
    public float Speed = 4f;
    public Vector3 Direction = Vector3.right;
    public bool IsMoving = true;
    public SnakeBodyMover SnakeBodyMover;

    [Inject]
    private readonly IScoreService _scoreService;
    private IEnumerator _moveRoutine;

    public void TurnLeft(InputAction.CallbackContext context = default)
    {
        if (!context.started)
        {
            return;
        }

        Direction = Quaternion.AngleAxis(-90, Vector3.up) * Direction;
        _scoreService.RegisterTurn();
    }

    public void TurnRight(InputAction.CallbackContext context = default)
    {
        if(!context.started)
        {
            return;
        }

        Direction = Quaternion.AngleAxis(90, Vector3.up) * Direction;
        _scoreService.RegisterTurn();
    }

    public void StartMoving()
    {
        if(_moveRoutine != null)
        {
            return;
        }

        IsMoving = true;
        _moveRoutine = MoveSnake();
        StartCoroutine(_moveRoutine);
    }

    public void StopMoving()
    {
        if(_moveRoutine == null)
        {
            return;
        }

        IsMoving = false;
        StopCoroutine(_moveRoutine);
        _moveRoutine = null;
    }

    IEnumerator MoveSnake()
    {
        while(IsMoving)
        {
            transform.localPosition += Direction;
            SnakeBodyMover.UpdateBodyTransforms(this);
            yield return new WaitForSeconds(1f/Speed);
        }
    }

    public void Kill()
    {
        StopMoving();
    }

    public void EatFood()
    {
        Length++;
        _scoreService.RegisterEat();
    }
}
