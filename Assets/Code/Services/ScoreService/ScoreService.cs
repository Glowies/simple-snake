using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreService : IScoreService
{
    public int Length { get; set; } = 5;

    public int Score { get; private set; } = 0;

    public int Combo { get; private set; } = 0;

    private int _turnCount = 0;

    public void RegisterEat()
    {
        Length++;
        Combo++;

        if(_turnCount < 4)
        {
            Score += 4 * Mathf.Min(Combo, 10);
        }
        else if(_turnCount < 6)
        {
            Score += 2 * Mathf.Min(Combo, 10);
        }
        else
        {
            Score += 1;
        }

        _turnCount = 0;
    }

    public void RegisterTurn()
    {
        _turnCount++;
    }
}
