using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    private FadeTransition fadeTransition;
    private Text timerText;
    private StageSelect stageSelect;

    private float duration = 90f; 
    private float remainingTime; 
    private bool isTimerRunning = true;
    private bool isPlayerLost;
    private int lossesCount = 0;
    private int currentStageIndex;
    private string currentDifficulty;
    public GameObject loseGameObject;
    public GameObject winGameObject;
    public GameObject warningGameObject;
    private void Start()
    {
        timerText = GetComponent<Text>();
        remainingTime = duration;
        UpdateTimerText();

        fadeTransition = FindObjectOfType<FadeTransition>();
        stageSelect = FindObjectOfType<StageSelect>();
        lossesCount = PlayerPrefs.GetInt("LossesCount", 0);
        currentStageIndex = PlayerPrefs.GetInt("SelectedStage", 0);
        currentDifficulty = PlayerPrefs.GetString("Difficulty");

    }

    private void Update()
    {
        if (!isTimerRunning)
            return;

        ScoreText scoreText = FindObjectOfType<ScoreText>();
        if (scoreText != null && scoreText.score >= 500)
        {
            isTimerRunning = false;
            CheckWinCondition();
            return;
        }
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
            StoreStageCompletionTime(currentStageIndex, currentDifficulty);
            isPlayerLost = false;
            PlayerPrefs.SetInt("PlayerLost", isPlayerLost ? 1 : 0);
            winGameObject.SetActive(true);
            fadeTransition.TogglePause();
            scoreText.SetPlayerMoney();
        }
        else
        {
            isPlayerLost = true;
            PlayerPrefs.SetInt("PlayerLost", isPlayerLost ? 1 : 0);
            lossesCount++;
            PlayerPrefs.SetInt("LossesCount", lossesCount);
            if (lossesCount >= 3)
            {
                warningGameObject.SetActive(true);
                fadeTransition.TogglePause();
            }
            else
            {
                Debug.Log(lossesCount);
                SpawnLoseGameObject();
                fadeTransition.TogglePause();
            }
        }
    }
    private void StoreStageCompletionTime(int stageIndex, string selectedDifficulty)
    {
        float completionTime = duration - remainingTime;

        string completionTimeKey = "StageCompletionTime_" + stageIndex;
        string serializedTimes = PlayerPrefs.GetString(completionTimeKey, "");

        if (!string.IsNullOrEmpty(serializedTimes))
        {
            serializedTimes += ",";
        }

        string completionInfo = completionTime.ToString() + "_" + selectedDifficulty.ToString();
        serializedTimes += completionInfo;
        PlayerPrefs.SetString(completionTimeKey, serializedTimes);
    }

    public void ResetLoseCount()
    {
        PlayerPrefs.SetInt("LossesCount", 0);
    }
    private void SpawnLoseGameObject()
    {
        if (loseGameObject != null)
        {
            loseGameObject.SetActive(true);
        }
    }
}