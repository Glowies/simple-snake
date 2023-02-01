using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Zenject;

public class Snake : MonoBehaviour
{
    public int Length = 1;
    public float Speed = 4f;
    public Vector3 Direction = Vector3.right;
    public bool IsMoving = true;
    public float DeathBlinkFrequency = 2f;
    public int InputBufferLimit = 3;
    public SnakeBodyMover SnakeBodyMover;

    [Inject]
    private readonly IScoreService _scoreService;
    private Renderer _renderer;
    private IEnumerator _moveRoutine;
    private IEnumerator _deathAnimationRoutine;
    private Vector3 _startPosition;
    private int _startLength;
    private float _startSpeed;
    private Vector3 _startDirection;
    private Queue<UnityAction> _inputBuffer;

    private void Awake()
    {
        TryGetComponent(out _renderer);
        _startPosition = transform.localPosition;
        _startLength = Length;
        _startSpeed = Speed;
        _startDirection = Direction;

        Reset();
    }

    private void EnqueueInput(UnityAction input)
    {
        if(_inputBuffer.Count >= InputBufferLimit)
        {
            return;
        }

        _inputBuffer.Enqueue(input);
    }

    private void PerformTurn(float eulerAngle)
    {
        // Turn snake
        var rotation = Quaternion.AngleAxis(eulerAngle, Vector3.up);
        Direction = rotation * Direction;

        // Register turn
        _scoreService.RegisterTurn();
    }

    public void PerformTurnLeft() => PerformTurn(-90);
    public void PerformTurnRight() => PerformTurn(90);

    public void TurnLeft() => EnqueueInput(PerformTurnLeft);
    public void TurnRight() => EnqueueInput(PerformTurnRight);

    public void TurnLeft(InputAction.CallbackContext context = default)
    {
        if (!context.started)
        {
            return;
        }

        TurnLeft();
    }

    public void TurnRight(InputAction.CallbackContext context = default)
    {
        if(!context.started)
        {
            return;
        }

        TurnRight();
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
            // Perform turns in input buffer
            if(_inputBuffer.Count > 0)
            {
                _inputBuffer.Dequeue().Invoke();
            }

            // Move the snake
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
            ToggleRenderer(false);

            yield return new WaitForSeconds(waitTime);

            ToggleRenderer(true);

            yield return new WaitForSeconds(waitTime);
        }
    }

    public void ToggleEnabled(bool state)
    {
        gameObject.SetActive(state);
        SnakeBodyMover.ToggleAllEnabled(state);
    }

    public void ToggleRenderer(bool state)
    {
        _renderer.enabled = state;
        SnakeBodyMover.ToggleAllRenderers(state);
    }
    
    public void Reset()
    {
        // Initialize Input Buffer
        _inputBuffer = new();
        
        transform.localPosition = _startPosition;
        Length = _startLength;
        Speed = _startSpeed;
        Direction = _startDirection;
        SnakeBodyMover.Reset();
    }

    public void Kill()
    {
        StopMoving();
        PlayDeathAnimation(DeathBlinkFrequency);
    }

    public void EatFood()
    {
        Length++;
        _scoreService.RegisterEat();
        StartCoroutine(BumpSnake());
    }
}
