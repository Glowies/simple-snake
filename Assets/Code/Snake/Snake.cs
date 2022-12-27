using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Snake : MonoBehaviour
{
    public int Length = 1;
    public float Speed = 4f;
    public Vector3 Direction = Vector3.right;
    public bool IsMoving = true;
    public SnakeBodyMover SnakeBodyMover;

    private IEnumerator _moveRoutine;

    private void Start()
    {
        //StartMoving();
    }

    public void TurnLeft(InputAction.CallbackContext context = default)
    {
        if (!context.started)
        {
            return;
        }

        Direction = Quaternion.AngleAxis(-90, Vector3.up) * Direction;
    }

    public void TurnRight(InputAction.CallbackContext context = default)
    {
        if(!context.started)
        {
            return;
        }

        Direction = Quaternion.AngleAxis(90, Vector3.up) * Direction;
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
    }
}
