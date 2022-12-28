using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridService : IGridService
{
    public int Width
    {
        get;
        set;
    } = 16;

    public int Height
    {
        get;
        set;
    } = 16;

    public float CellSize
    {
        get;
        set;
    } = 1f;

    public Vector3 BottomLeftCorner
    {
        get;
        set;
    } = new Vector3(-7.5f, 0, -7.5f);

    public Vector3 GetCellPosition(int x, int y)
    {
        var offset = new Vector3(x, 0, y) * CellSize;
        return BottomLeftCorner + offset;
    }

    public Vector3 GetRandomCellPosition()
    {
        var randX = Random.Range(0, Width);
        var randY = Random.Range(0, Height);
        return GetCellPosition(randX, randY);
    }
}