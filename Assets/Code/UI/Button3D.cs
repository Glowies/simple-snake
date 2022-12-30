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
    public UnityEvent OnTouched;

    private Collider _collider;

    private void Awake()
    {
        TryGetComponent(out _collider);
    }

    // Update is called once per frame
    void Update()
    {
        var touchCount = TouchCount();
        for(int i=0; i<touchCount; i++)
        {
            OnTouched.Invoke();
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

    private bool IsPositionOnCollider(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        return _collider.Raycast(ray, out var hit, 1024);
    }
}
