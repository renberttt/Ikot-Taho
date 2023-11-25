using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    private FadeTransition fadeTransition;
    private Text timerText;

    private float duration = 2 * 60f; 
    private float remainingTime; 
    private bool isTimerRunning = true;

    public GameObject loseGameObject;
    public GameObject winGameObject;

    private void Start()
    {
        timerText = GetComponent<Text>();
        remainingTime = duration;
        UpdateTimerText();

        if(fadeTransition == null)
        {
            fadeTransition = FindObjectOfType<FadeTransition>();
        }
    }

    private void Update()
    {
        if (!isTimerRunning)
            return;

        if (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            isTimerRunning = false;
            CheckWinCondition();
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        // Check if the timer has reached 00:00
        if (minutes <= 0 && seconds <= 0)
        {
            minutes = 0;
            seconds = 0;
        }

        string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timeText;
    }

    public float GetRemainingTime()
    {
        return remainingTime;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    private void CheckWinCondition()
    {
        ScoreText scoreText = FindObjectOfType<ScoreText>();

        if (scoreText != null && scoreText.score >= 500)
        {
            winGameObject.SetActive(true);
            fadeTransition.TogglePause();
        }
        else
        {
            SpawnLoseGameObject();
            fadeTransition.TogglePause();
        }
    }

    private void SpawnLoseGameObject()
    {
        if (loseGameObject != null)
        {
            loseGameObject.SetActive(true);
        }
    }
}
