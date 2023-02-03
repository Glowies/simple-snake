using System;

public record SnakeStatistics
{
    public int Length = 0;
    public int Score = 0;
    public int Combo = 0;
    public int MaxCombo = 0;
}

public interface IScoreService
{
    SnakeStatistics Statistics { get; }

    int Length { get; }
    int Score { get; }
    int Combo { get; }
    int MaxCombo { get; }

    void RegisterEat();
    void RegisterTurn();
    void Reset();
}

