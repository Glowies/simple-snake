using System;

public interface IScoreService
{
    int Length { get; }
    int Score { get; }
    int Combo { get; }
    int MaxCombo { get; }

    void RegisterEat();
    void RegisterTurn();
    void Reset();
}

