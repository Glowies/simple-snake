using System;
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
    public float HeightBias = -0.01f;

    private Queue<TransformCopyValues> _bodyTransforms;
    private List<GameObject> _bodyParts;
    private TransformCopyValues _defaultTransform;
    
    [Zenject.Inject]
    private readonly SnakeBody.Factory _bodyFactory;


    void Awake()
    {
        Reset();
        _defaultTransform = new()
        {
            localPosition = Vector3.up * 1327,
            localRotation = Quaternion.identity,
            localScale = Vector3.zero
        };
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
            var refIndex = i;
            if(refIndex > _bodyTransforms.Count - 2)
            {
                CopyTransform(in _defaultTransform, ref bodyPartTransform);
                continue;
            }
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
        var newPos = refTransform.localPosition + Vector3.up * HeightBias;
        targetTransform.localPosition = newPos;
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
            var newBody = _bodyFactory.Create(BodyPrefab).gameObject;
            var newTransform = newBody.transform;
            newTransform.SetParent(snake.transform.parent);
            CopyTransform(_defaultTransform, ref newTransform);
            newBody.name = $"Body{_bodyParts.Count}";
            _bodyParts.Add(newBody);
        }
    }

    public void Reset()
    {
        if(_bodyParts != null)
        {
            foreach(var bodyPart in _bodyParts)
            {
                DestroyImmediate(bodyPart);
            }
        }
        
        _bodyTransforms = new();
        _bodyParts = new();
    }

    public void ToggleAllRenderers(bool state)
    {
        foreach(var bodyPart in _bodyParts)
        {
            bodyPart.TryGetComponent<Renderer>(out var renderer);
            renderer.enabled = state;
        }
    }

    public void ToggleAllEnabled(bool state)
    {
        foreach(var bodyPart in _bodyParts)
        {
            bodyPart.SetActive(state);
        }
    }
}
