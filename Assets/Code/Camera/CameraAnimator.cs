using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAnimator : MonoBehaviour
{
    public float ZoomSpeed = 1;
    public Vector3 ZoomedInPosition;
    public Vector3 ZoomedInRotation;

    public Vector3 ZoomedOutPosition;
    public Vector3 ZoomedOutRotation;

    private IEnumerator ZoomAnimation(Vector3 position, Quaternion rotation)
    {
        float t = 0;
        var startPosition = transform.localPosition;
        var startRotation = transform.localRotation;

        while(t<1)
        {
            t += Time.deltaTime * ZoomSpeed;

            var tSin = Mathf.Sin(t * Mathf.PI / 2f);

            var newPos = Vector3.Lerp(startPosition, position, tSin);
            transform.localPosition = newPos;
            var newRot = Quaternion.Lerp(startRotation, rotation, tSin);
            transform.localRotation = newRot;

            yield return new WaitForEndOfFrame();
        }
    }

    [ContextMenu("Zoom In")]
    public void ZoomIn()
    {
        StopAllCoroutines();
        var position = ZoomedInPosition;
        var rotation = Quaternion.Euler(ZoomedInRotation);
        StartCoroutine(ZoomAnimation(position, rotation));
    }

    [ContextMenu("Zoom Out")]
    public void ZoomOut()
    {
        StopAllCoroutines();
        var position = ZoomedOutPosition;
        var rotation = Quaternion.Euler(ZoomedOutRotation);
        StartCoroutine(ZoomAnimation(position, rotation));
    }
}
