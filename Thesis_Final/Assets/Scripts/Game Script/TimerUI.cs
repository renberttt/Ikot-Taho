using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    private FadeTransition fadeTransition;
    private MainGameController mainGameController;
    private Text timerText;

    private float duration = 90f; 
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
        if(mainGameController == null)
        {
            mainGameController = FindObjectOfType<MainGameController>();
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

        if (minutes <= 0 && seconds <= 0)
        {
            minutes = 0;
            seconds = 0;
        }

        string timeText = string.Format("{0:0}:{1:00}", minutes, seconds);
        timerText.text = timeText;
    }

    public float GetRemainingTime()
    {
        return remainingTime;
    }

    public void StopTimer(bool check)
    {
        isTimerRunning = check;
    }

    private void CheckWinCondition()
    {
        ScoreText scoreText = FindObjectOfType<ScoreText>();

        if (scoreText != null && scoreText.score >= 500)
        {
            winGameObject.SetActive(true);
            fadeTransition.TogglePause();
            scoreText.SetPlayerMoney();
            mainGameController.GetPlayerStatus(1);
        }
        else
        {
            SpawnLoseGameObject();
            fadeTransition.TogglePause();
            mainGameController.GetPlayerStatus(0);
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
