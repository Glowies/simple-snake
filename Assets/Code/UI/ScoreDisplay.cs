using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI LengthText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI MaxComboText;

    [Zenject.Inject]
    private IScoreService _scoreService;

    void OnEnable() => UpdateTextDisplay();

    public void UpdateTextDisplay()
    {
        var length = _scoreService.Length;
        var score = _scoreService.Score;
        var maxCombo = _scoreService.MaxCombo;

        LengthText.text = length.ToString();
        ScoreText.text = score.ToString();
        MaxComboText.text = maxCombo.ToString();
    }
}
