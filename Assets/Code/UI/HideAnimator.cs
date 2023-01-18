using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAnimator : MonoBehaviour
{
    public Transform HideTarget;
    public Vector3 HideOffset = Vector3.right;
    public float HideDuration = .3f;

    private Vector3 _startPosition;

    private void Awake()
    {
        if(HideTarget != null)
        {
            _startPosition = HideTarget.localPosition;
        }
    }

    [ContextMenu("Show")]
    public void Show()
    {
        StopAllCoroutines();
        StartCoroutine(ShowAnimation());
    }

    [ContextMenu("Hide")]
    public void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(HideAnimation());
    }

    private IEnumerator HideAnimation()
    {
        float t = 0;
        var hidePosition = _startPosition + HideOffset;
        var currPosition = HideTarget.localPosition;
        var hideSpeed = 1f / HideDuration;

        while(t < 1)
        {
            t += Time.deltaTime * hideSpeed;
            HideTarget.localPosition = Vector3.Lerp(currPosition, hidePosition, t);
            yield return new WaitForEndOfFrame();
        }
        HideTarget.gameObject.SetActive(false);
    }

    private IEnumerator ShowAnimation()
    {
        float t = 0;
        var showPosition = _startPosition;
        var currPosition = HideTarget.localPosition;
        var hideSpeed = 1f / HideDuration;

        HideTarget.gameObject.SetActive(true);
        while(t < 1)
        {
            t += Time.deltaTime * hideSpeed;
            HideTarget.localPosition = Vector3.Lerp(currPosition, showPosition, t);
            yield return new WaitForEndOfFrame();
        }
    }
}
