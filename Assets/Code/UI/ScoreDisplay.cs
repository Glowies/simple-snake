using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI LengthText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI ComboText;

    private IScoreService _scoreService;

    void OnEnable()
    {
        print("test");
    }
}
