using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreService : IHighScoreService
{
    private const string _PBLengthKey = "PB_Length";
    private const string _PBScoreKey = "PB_Score";
    private const string _PBComboKey = "PB_Combo";
    private const string _PBMaxComboKey = "PB_MaxCombo";

    [Zenject.Inject]
    private IScoreService _scoreService;

    [Zenject.Inject]
    private ILongTermStorage _lts;

    public SnakeStatistics GetPersonalBests()
    {
        var result = new SnakeStatistics()
        {
            Score = _lts.GetInt(_PBScoreKey),
            Length = _lts.GetInt(_PBLengthKey),
            Combo = _lts.GetInt(_PBComboKey),
            MaxCombo = _lts.GetInt(_PBMaxComboKey),
        };

        return result;
    }

    public void RecordCurrentScore()
    {
        var stats = _scoreService.Statistics;
        var bests = GetPersonalBests();

        if(stats.Score > bests.Score)
        {
            _lts.SetInt(_PBScoreKey, stats.Score);
        }

        if(stats.Length > bests.Length)
        {
            _lts.SetInt(_PBLengthKey, stats.Length);
        }

        if(stats.Combo > bests.Combo)
        {
            _lts.SetInt(_PBComboKey, stats.Combo);
        }

        if(stats.MaxCombo > bests.MaxCombo)
        {
            _lts.SetInt(_PBMaxComboKey, stats.MaxCombo);
        }
    }
}
