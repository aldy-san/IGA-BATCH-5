using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIScoreController : MonoBehaviour
{

    [Header("UI")]
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;

    [Header("Score")]
    public ScoreController scoreController;

    private void Update()
    {
        score.text = scoreController.GetCurrentScore().ToString();
        highScore.text = ScoreData.highScore.ToString();
    }
}
