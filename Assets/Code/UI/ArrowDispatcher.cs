using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowDispatcher : MonoBehaviour
{
    public Button3D LeftArrowButton;
    public Button3D RightArrowButton;

    public void OnNavigate(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();

        if(direction.x > .5)
        {
            TriggerRight();
        }
        else if(direction.x < -.5)
        {
            TriggerLeft();
        }
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if(!context.started)
        {
            return;
        }

        TriggerLeft();
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        if(!context.started)
        {
            return;
        }

        TriggerRight();
    }

    public void TriggerRight()
    {
        RightArrowButton.TriggerOnce();
    }

    public void TriggerLeft()
    {
        LeftArrowButton.TriggerOnce();
    }
}
