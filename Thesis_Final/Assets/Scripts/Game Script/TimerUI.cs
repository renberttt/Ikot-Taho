using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public float duration = 300f; 
    private float remainingTime; 
    private Text timerText;

    private bool isTimerRunning = true;

    public GameObject loseGameObject;
    public GameObject winObjectPrefab;

    private void Start()
    {
        timerText = GetComponent<Text>();
        remainingTime = duration;
        UpdateTimerText();
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
            Debug.Log("Time's up!");
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
            Instantiate(winObjectPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            Debug.Log("You Win!");
            PauseGame();
        }
        else
        {
            SpawnLoseGameObject();
            Debug.Log("You Lose!");
            PauseGame();
        }
    }

    private void SpawnLoseGameObject()
    {
        if (loseGameObject != null)
        {
            Instantiate(loseGameObject, new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
    }

    private void PauseGame()
    {
        // Pause the game by setting the time scale to 0
        Time.timeScale = 0f;
    }
}
