using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuThumbnail : MonoBehaviour
{
    public int FaceCount = 3;
    public int SelectedFace = 1;
    public bool HasHomeFace = false;

    public Vector3 PositionDelta = Vector3.right;

    public GameObject ThumbnailPrefab;
    public GameObject SelectedThumbnail;

    private List<GameObject> _thumbnails;

    private void Awake()
    {
        _thumbnails = new();
    }

    [ContextMenu("Update Display")]
    public void UpdateDisplay()
    {
        AddRemoveThumbnails();
        UpdatePositions();
    }

    private void AddRemoveThumbnails()
    {
        var targetLength = FaceCount - 1;
        var diff = _thumbnails.Count - targetLength;
        if (diff >= 0)
        {
            // Destroy excess body parts
            for (int i = 0; i < diff; i++)
            {
                var toRemove = _thumbnails[targetLength + i];
                DestroyImmediate(toRemove);
            }

            _thumbnails.RemoveRange(targetLength, diff);
            return;
        }

        // Add new body parts
        diff *= -1;
        for(int i=0; i<diff; i++)
        {
            var newBody = Instantiate(ThumbnailPrefab, transform);
            newBody.name = $"Thumbnail{_thumbnails.Count}";
            _thumbnails.Add(newBody);
        }
    }

    private void UpdatePositions()
    {
        var factor = ((float)FaceCount - 1) / 2f;
        var startPoint = -PositionDelta * factor;

        var offset = 0;
        for(int i=0; i<FaceCount; i++)
        {
            if(i == SelectedFace)
            {
                offset = -1;
                var selectedPosition = startPoint + PositionDelta * i;
                SelectedThumbnail.transform.localPosition = selectedPosition;
                continue;
            }

            var thumbnail = _thumbnails[i+offset];
            var position = startPoint + PositionDelta * i;
            thumbnail.transform.localPosition = position;
        }
    }
}
