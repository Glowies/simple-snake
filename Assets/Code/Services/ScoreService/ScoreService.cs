using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreService : MonoBehaviour, IScoreService
{
    public TextMeshProUGUI LengthText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI ComboText;

    public SnakeStatistics Statistics { get; private set; } = new SnakeStatistics();

    public int Length { 
        get => Statistics.Length;
        set => Statistics.Length = value;
    }

    public int Score { 
        get => Statistics.Score;
        private set => Statistics.Score = value;
    }

    public int Combo { 
        get => Statistics.Combo;
        private set => Statistics.Combo = value;
    }

    public int MaxCombo { 
        get => Statistics.MaxCombo;
        private set => Statistics.MaxCombo = value;
    }

    private int _turnCount = 0;

    public void RegisterEat()
    {
        Length++;
        Combo++;
        MaxCombo = Mathf.Max(MaxCombo, Combo);

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

        UpdateTextDisplay();
    }

    public void RegisterTurn()
    {
        _turnCount++;

        if(_turnCount > 5)
        {
            Combo = 0;
            UpdateTextDisplay();
        }
    }

    public void UpdateTextDisplay()
    {
        LengthText.text = Length.ToString();
        ScoreText.text = Score.ToString();
        ComboText.text = $"x{Combo}";
    }

    public void Reset()
    {
        Length = 0;
        Score = 0;
        Combo = 0;
        MaxCombo = 0;
        UpdateTextDisplay();
    }
}
