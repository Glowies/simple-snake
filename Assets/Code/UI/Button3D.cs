using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

[RequireComponent(typeof(Collider))]
public class Button3D : MonoBehaviour
{
    public Transform BumpTarget;
    public Vector3 BumpOffset = Vector3.right;
    public float BumpRetreatSpeed = 2f;

    public UnityEvent OnTouched;

    private Collider _collider;
    private Vector3 _startPosition;

    private void Awake()
    {
        TryGetComponent(out _collider);
        _startPosition = BumpTarget.localPosition;
    }

    void Update()
    {
        var touchCount = TouchCount();
        for(int i=0; i<touchCount; i++)
        {
            OnTouched.Invoke();
        }

        if(touchCount > 0)
        {
            StopAllCoroutines();
            StartCoroutine(BumpAnimation());
        }
    }

    private bool IsTouched()
    {
        return TouchCount() > 0;
    }

    private int TouchCount()
    {
        int result = 0;

        if (Mouse.current.leftButton.wasPressedThisFrame &&
            IsPositionOnCollider(Mouse.current.position.ReadValue()))
        {
            result++;
        }

        
        foreach (Touch touch in Touch.activeTouches)
        {
            if (touch.phase == TouchPhase.Began &&
                IsPositionOnCollider(touch.screenPosition))
            {
                result++;
            }
        }

        return result;
    }

    public void TriggerOnce()
    {
        OnTouched.Invoke();

        StopAllCoroutines();
        StartCoroutine(BumpAnimation());
    }

    private bool IsPositionOnCollider(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        return _collider.Raycast(ray, out var hit, 1024);
    }

    private IEnumerator BumpAnimation()
    {
        float t = 0;
        var bumpTarget = _startPosition + BumpOffset;
        while(t < 1)
        {
            t += Time.deltaTime * BumpRetreatSpeed;
            BumpTarget.localPosition = Vector3.Lerp(bumpTarget, _startPosition, t);
            yield return new WaitForEndOfFrame();
        }
    }
}
