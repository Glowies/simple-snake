using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimator : MonoBehaviour
{
    public Vector3 Direction;
    public Vector3 OriginOffset;
    public float AnimationSpeed = 4f;
    public float SpreadFactor = 2f;

    private IEnumerator _animationRoutine;
    private List<Transform> _children;
    private List<Vector3> _startPositions;
    private List<float> _tOffsets;

    private void Awake()
    {
        PreprocessChildren();
    }

    private void PreprocessChildren()
    {
        _children = new();
        _tOffsets = new();
        _startPositions = new();

        var tMax = float.NegativeInfinity;
        var tMin = float.PositiveInfinity;
        foreach(Transform child in transform)
        {
            // Add children and positions to list
            _children.Add(child);
            _startPositions.Add(child.localPosition);

            // Calculate offset
            var tOffset = Vector3.Dot(child.localPosition, Direction);

            // Add to offset list
            _tOffsets.Add(tOffset);

            // Update min and max
            tMax = Mathf.Max(tMax, tOffset);
            tMin = Mathf.Min(tMin, tOffset);
        }

        // Normalize t offsets
        var range = tMax - tMin;
        for(int i=0; i<_tOffsets.Count; i++)
        {
            _tOffsets[i] = SpreadFactor * (_tOffsets[i] - tMax) / range;
        }
    }

    public void PlayInAnimation()
    {
        if(_animationRoutine != null)
        {
            StopCoroutine(_animationRoutine);
        }

        _animationRoutine = InAnimation();
        StartCoroutine(_animationRoutine);
    }

    public void PlayOutAnimation()
    {
        if (_animationRoutine != null)
        {
            StopCoroutine(_animationRoutine);
        }

        _animationRoutine = OutAnimation();
        StartCoroutine(_animationRoutine);

    }

    private IEnumerator InAnimation()
    {
        var hasLessThanOne = true;
        float t = 0;
        while (hasLessThanOne)
        {
            hasLessThanOne = false;
            t += Time.deltaTime * AnimationSpeed;
            for (int i = 0; i < _children.Count; i++)
            {
                var tFinal = t + _tOffsets[i];
                var startPos = _startPositions[i];
                var originPoint = OriginOffset + startPos;
                var newPos = Vector3.Lerp(originPoint, startPos, tFinal);
                _children[i].localPosition = newPos;

                if (tFinal < 1)
                {
                    hasLessThanOne = true;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator OutAnimation()
    {
        var hasLessThanOne = true;
        float t = 0;
        while (hasLessThanOne)
        {
            hasLessThanOne = false;
            t += Time.deltaTime * AnimationSpeed;
            for (int i = 0; i < _children.Count; i++)
            {
                var tFinal = t + _tOffsets[i];
                var startPos = _startPositions[i];
                var originPoint = OriginOffset + startPos;
                var newPos = Vector3.Lerp(startPos, originPoint, tFinal);
                _children[i].localPosition = newPos;

                if (tFinal < 1)
                {
                    hasLessThanOne = true;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
