using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public int ActiveFace {
        get
        {
            return _activeFace;
        }
        set
        {
            _activeFace = MathMod(value, _menuFaces.Count);
            ShowActiveFace();
        }
    }

    [SerializeField]
    private int _startFace;
    private int _activeFace;
    private List<GameObject> _menuFaces;

    // Start is called before the first frame update
    void Start()
    {
        _menuFaces = new();
        foreach(Transform child in transform)
        {
            _menuFaces.Add(child.gameObject);
        }

        ActiveFace = _startFace;
    }

    public void ShowActiveFace()
    {
        HideAllFaces();
        var face = _menuFaces[ActiveFace];
        face.SetActive(true);

        var animators = face.GetComponentsInChildren<BlockAnimator>();
        foreach(var animator in animators)
        {
            animator.PlayInAnimation();
        }
    }

    public void HideAllFaces()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void LeftPage()
    {
        ActiveFace--;
    }

    public void RightPage()
    {
        ActiveFace++;
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();

        if(direction.x > .5)
        {
            RightPage();
        }
        else if(direction.x < -.5)
        {
            LeftPage();
        }
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        if(!context.started)
        {
            return;
        }

        TriggerActiveFace();
    }

    public void TriggerActiveFace()
    {
        var face = _menuFaces[ActiveFace];
        if(!face.TryGetComponent(out Button3D button))
        {
            return;
        }

        button.TriggerOnce();
    }

    private int MathMod(int a, int b)
    {
        return (Mathf.Abs(a * b) + a) % b;
    }
}
