using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;

    public int player1Score;
    public int player2Score;

    void Start()
    {
        player1Score = 0;
        player2Score = 0;
        UpdateScoreText();
    }

    public void AddScore(bool isPlayer2, int scoreToAdd)
    {
        if (isPlayer2)
        {
            player2Score += scoreToAdd;
        }
        else
        {
            player1Score += scoreToAdd;
        }
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        player1ScoreText.text = "Score: " + player1Score.ToString();
        player2ScoreText.text = "Score: " + player2Score.ToString();
    }
}

