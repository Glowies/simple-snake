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
            RightArrowButton.TriggerOnce();
        }
        else if(direction.x < -.5)
        {
            LeftArrowButton.TriggerOnce();
        }
    }
}
