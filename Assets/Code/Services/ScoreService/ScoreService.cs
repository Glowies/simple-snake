using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreService : MonoBehaviour, IScoreService
{
    public TextMeshProUGUI LengthText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI ComboText;
    public int Length { get; set; } = 0;
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

        UpdateTextDisplay();
    }

    public void RegisterTurn()
    {
        _turnCount++;
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
        UpdateTextDisplay();
    }
}
