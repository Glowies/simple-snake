using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    public GameObject ZoomedInCamera;

    public GameObject ZoomedOutCamera;

    private bool _isZoomedIn;

    [ContextMenu("Zoom In")]
    public void ZoomIn()
    {
        _isZoomedIn = true;
        UpdateCameras();
    }

    [ContextMenu("Zoom Out")]
    public void ZoomOut()
    {
        _isZoomedIn = false;
        UpdateCameras();
    }

    private void UpdateCameras()
    {
        ZoomedInCamera.SetActive(_isZoomedIn);
        ZoomedOutCamera.SetActive(!_isZoomedIn);
    }
}
