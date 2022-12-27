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
    public float DeathBlinkFrequency = 2f;
    public SnakeBodyMover SnakeBodyMover;

    [Inject]
    private readonly IScoreService _scoreService;
    private Renderer _renderer;
    private IEnumerator _moveRoutine;
    private IEnumerator _deathAnimationRoutine;

    private void Awake()
    {
        TryGetComponent(out _renderer);
    }

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
        StopDeathAnimation();
        SnakeBodyMover.StopDeathAnimation();
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

    IEnumerator BumpSnake()
    {
        var bumpOffset = Vector3.up * 0.2f;
        transform.localPosition += bumpOffset;

        yield return new WaitForSeconds(0.1f);

        transform.localPosition -= bumpOffset;
    }

    public void PlayDeathAnimation(float blinkFrequency)
    {
        if (_deathAnimationRoutine != null)
        {
            return;
        }

        _deathAnimationRoutine = DeathAnimation(blinkFrequency);
        StartCoroutine(_deathAnimationRoutine);
    }

    public void StopDeathAnimation()
    {
        if (_deathAnimationRoutine == null)
        {
            return;
        }

        StopCoroutine(_deathAnimationRoutine);
        _deathAnimationRoutine = null;
        _renderer.enabled = true;
    }
    private IEnumerator DeathAnimation(float blinkFrequency)
    {
        var waitTime = 1f / blinkFrequency / 2f;

        while (true)
        {
            _renderer.enabled = false;

            yield return new WaitForSeconds(waitTime);

            _renderer.enabled = true;

            yield return new WaitForSeconds(waitTime);
        }
    }

    public void Kill()
    {
        StopMoving();
        PlayDeathAnimation(DeathBlinkFrequency);
        SnakeBodyMover.PlayDeathAnimation(DeathBlinkFrequency);
    }

    public void EatFood()
    {
        Length++;
        _scoreService.RegisterEat();
        StartCoroutine(BumpSnake());
    }
}
