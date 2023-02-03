using System;

public interface IHighScoreService
{
    void RecordCurrentScore();

    SnakeStatistics GetPersonalBests();
}

