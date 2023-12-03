using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DisplayTime : MonoBehaviour
{
    public TMP_Text[] timelog;
    public TMP_Text collegeName;
    public TMP_Text difficultyName;
    private int selectedStageIndex = 0;
    private string currentDifficulty;

    void Start()
    {
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        selectedStageIndex = PlayerPrefs.GetInt("SelectedStage", 0);
        currentDifficulty = PlayerPrefs.GetString("Difficulty", "Easy");

        DisplayStageCompletionTimes(selectedStageIndex, currentDifficulty);
        difficultyName.text = currentDifficulty;
        collegeName.text = GetSelectedButtonName(selectedStageIndex);
    }

    private void DisplayStageCompletionTimes(int stageIndex, string difficulty)
    {
        string completionTimeKey = "StageCompletionTime_" + stageIndex;

        string serializedTimes = PlayerPrefs.GetString(completionTimeKey, "");
        if (!string.IsNullOrEmpty(serializedTimes))
        {
            List<string> displayedTimes = new List<string>();

            string[] completionInfos = serializedTimes.Split(',');

            foreach (string completionInfo in completionInfos)
            {
                string[] infoParts = completionInfo.Split('_');
                if (infoParts.Length == 2 && infoParts[1] == difficulty)
                {
                    float time;
                    if (float.TryParse(infoParts[0], out time))
                    {
                        displayedTimes.Add(FormatTime(time));
                    }
                }
            }

            for (int i = 0; i < timelog.Length; i++)
            {
                if (i < displayedTimes.Count)
                {
                    timelog[i].text = displayedTimes[i];
                }
                else
                {
                    timelog[i].text = "N/A";
                }
            }
        }
        else
        {
            for (int i = 0; i < timelog.Length; i++)
            {
                timelog[i].text = "N/A";
            }
        }
    }

    private int GetLogIndexForDifficulty(string difficulty)
    {
        if (difficulty == "Easy")
        {
            return 0;
        }
        else if (difficulty == "Medium")
        {
            return 1;
        }
        else if (difficulty == "Hard")
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void OnButtonClick(Button button)
    {
        int index = System.Array.IndexOf(GetComponentsInChildren<Button>(), button);
        if (index >= 0 && index < 7)
        {
            selectedStageIndex = index;
            PlayerPrefs.SetInt("SelectedStage", selectedStageIndex);
        }

        UpdateDisplay();
    }

    public void SetDifficulty(string difficulty)
    {
        PlayerPrefs.SetString("Difficulty", difficulty);
        currentDifficulty = difficulty;
        UpdateDisplay();
    }

    private string GetSelectedButtonName(int stageIndex)
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        if (stageIndex >= 0 && stageIndex < buttons.Length)
        {
            return buttons[stageIndex].gameObject.name;
        }

        return "Unknown";
    }
}