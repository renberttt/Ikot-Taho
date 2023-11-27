using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public int score = 0;
    private Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<Text>();

        if (scoreText == null)
        {
            Debug.LogError("ScoreText script requires a Text component.");
        }

        UpdateScoreText();
    }

    public void IncrementScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = " " + score.ToString();
        }
    }

    public void GetPlayerMoney()
    {
        PlayerPrefs.SetInt("PlayerMoney", score);
    }
}
