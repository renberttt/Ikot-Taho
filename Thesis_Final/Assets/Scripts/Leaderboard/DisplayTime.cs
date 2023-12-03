using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DisplayTime : MonoBehaviour
{
    public TMP_Text[] timelog;
    public TMP_Text collegeName;
    private int selectedStageIndex = 0;
    private string currentDifficulty;

    void Start()
    {
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        selectedStageIndex = PlayerPrefs.GetInt("SelectedStage", 0);
        currentDifficulty = PlayerPrefs.GetString("Difficulty");

        DisplayStageCompletionTimes(selectedStageIndex, currentDifficulty);
        collegeName.text = GetSelectedButtonName(selectedStageIndex);
    }

    private void DisplayStageCompletionTimes(int stageIndex, string difficulty)
    {
        string completionTimeKey = "StageCompletionTime_" + stageIndex;

        string serializedTimes = PlayerPrefs.GetString(completionTimeKey, "");
        if (!string.IsNullOrEmpty(serializedTimes))
        {
            string[] completionTimes = serializedTimes.Split(',');

            for (int i = 0; i < timelog.Length; i++)
            {
                if (i < completionTimes.Length)
                {
                    float time;
                    if (float.TryParse(completionTimes[i], out time))
                    {
                        timelog[i].text = FormatTime(time);
                    }
                    else
                    {
                        timelog[i].text = "N/A";
                    }
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