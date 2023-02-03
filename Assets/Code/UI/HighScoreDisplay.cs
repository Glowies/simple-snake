using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI LengthText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI MaxComboText;

    [Zenject.Inject]
    private IHighScoreService _highScoreService;

    void OnEnable() => UpdateTextDisplay();

    public void UpdateTextDisplay()
    {
        var bests = _highScoreService.GetPersonalBests();
        var length = bests.Length;
        var score = bests.Score;
        var maxCombo = bests.MaxCombo;

        LengthText.text = length.ToString();
        ScoreText.text = score.ToString();
        MaxComboText.text = maxCombo.ToString();
    }
}
