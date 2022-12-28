using System;
using UnityEngine;

public interface IGridService
{
    int Width { get; set; }
    int Height { get; set; }
    float CellSize { get; set; }
    Vector3 BottomLeftCorner { get; set; }

    Vector3 GetRandomCellPosition();
    Vector3 GetCellPosition(int x, int y);
}

