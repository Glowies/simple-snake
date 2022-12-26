using System.Collections;
using System.Collections.Generic;
using UnityEngine;

record TransformCopyValues
{
    public Vector3 localPosition;
    public Vector3 localScale;
    public Quaternion localRotation;
}

public class SnakeBodyMover : MonoBehaviour
{
    public GameObject BodyPrefab;

    private Queue<TransformCopyValues> _bodyTransforms;
    private List<GameObject> _bodyParts;

    void Awake()
    {
        _bodyTransforms = new();
        _bodyParts = new();
    }

    public void UpdateBodyTransforms(Snake snake)
    {
        AddRemoveBodyParts(snake);

        // Add head transform to queue
        var transformCopy = GetCopyValues(snake.transform);
        _bodyTransforms.Enqueue(transformCopy);
        while(_bodyTransforms.Count > snake.Length)
        {
            _bodyTransforms.Dequeue();
        }

        // Get transforms array
        var bodyTransforms = _bodyTransforms.ToArray();

        for(int i=0; i<_bodyParts.Count; i++)
        {
            var bodyPartTransform = _bodyParts[i].transform;
            var refIndex = Mathf.Min(i + 1, _bodyTransforms.Count - 1);
            var refTransform = bodyTransforms[refIndex];
            CopyTransform(in refTransform, ref bodyPartTransform);
        }
    }

    TransformCopyValues GetCopyValues(Transform refTransform)
    {
        return new TransformCopyValues
        {
            localPosition = refTransform.localPosition,
            localScale = refTransform.localScale,
            localRotation = refTransform.localRotation,
        };
    }

    void CopyTransform(
        in TransformCopyValues refTransform,
        ref Transform targetTransform)
    {
        targetTransform.localPosition = refTransform.localPosition;
        targetTransform.localScale = refTransform.localScale;
        targetTransform.localRotation = refTransform.localRotation;
    }

    void AddRemoveBodyParts(Snake snake)
    {
        var targetLength = snake.Length - 1;
        var diff = _bodyParts.Count - targetLength;
        if (diff >= 0)
        {
            // Destroy excess body parts
            for (int i = 0; i < diff; i++)
            {
                var toRemove = _bodyParts[targetLength + i];
                DestroyImmediate(toRemove);
            }

            _bodyParts.RemoveRange(targetLength, diff);
            return;
        }

        // Add new body parts
        diff *= -1;
        for(int i=0; i<diff; i++)
        {
            var newBody = Instantiate(BodyPrefab, snake.transform.parent);
            newBody.name = $"Body{_bodyParts.Count}";
            _bodyParts.Add(newBody);
        }
    }
}
