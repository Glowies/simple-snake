using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Food : MonoBehaviour
{
    public float FlareDuration = 0.2f;

    [ColorUsage(true, true)]
    public Color FlareEmission;

    [Inject]
    private readonly IGridService _gridService;
    private bool _eatLock = false;
    private Material _foodMaterial;
    private Color _emissionDefault;
    private int _emissionColorId;

    private void Awake()
    {
        _foodMaterial = GetComponentInChildren<Renderer>().sharedMaterial;
        _emissionDefault = Color.black;
        _emissionColorId = Shader.PropertyToID("_EmissionColor");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SnakeBody body))
        {
            MoveToRandomCell();
            return;
        }

        Snake snake;

        if (!other.TryGetComponent(out snake) || _eatLock)
        {
            return;
        }

        _eatLock = true;
        snake.EatFood();
        MoveToRandomCell();
        StartCoroutine(SpawnAnimation());
        StartCoroutine(UnlockNextFrame());
    }

    private void MoveToRandomCell()
    {
        var newPos = _gridService.GetRandomCellPosition();
        newPos.y = transform.localPosition.y;
        transform.localPosition = newPos;
    }

    IEnumerator UnlockNextFrame()
    {
        yield return new WaitForEndOfFrame();
        _eatLock = false;
    }

    IEnumerator SpawnAnimation()
    {
        float t = 0;
        float flareSpeed = 1f/FlareDuration;
        while(t < 1)
        {
            t += Time.deltaTime * flareSpeed;
            float tSin = Mathf.Sin(t * Mathf.PI);

            var color = Color.Lerp(_emissionDefault, FlareEmission, tSin);
            _foodMaterial.SetColor(_emissionColorId, color);

            yield return new WaitForEndOfFrame();
        }
    }
}
