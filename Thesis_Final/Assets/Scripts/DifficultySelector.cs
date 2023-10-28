using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace

public class DifficultySelector : MonoBehaviour
{
    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }

    private DifficultyLevel selectedDifficulty;

    // Public fields to hold references to UI buttons
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    private void Start()
    {
        // Add event listeners to the UI buttons
        easyButton.onClick.AddListener(() => SetDifficulty(DifficultyLevel.Easy));
        mediumButton.onClick.AddListener(() => SetDifficulty(DifficultyLevel.Medium));
        hardButton.onClick.AddListener(() => SetDifficulty(DifficultyLevel.Hard));
    }

    public void SetDifficulty(DifficultyLevel difficulty)
    {
        selectedDifficulty = difficulty;
        // Optionally, you can store the selected difficulty in PlayerPrefs or another storage mechanism.
        // For example:
        // PlayerPrefs.SetString("SelectedDifficulty", selectedDifficulty.ToString());
    }
}
