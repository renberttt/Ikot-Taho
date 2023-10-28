using UnityEngine;

public class MainGameController : MonoBehaviour
{
    public GameObject[] backgrounds;
    public GameObject[] customerSets;
    public DifficultySelector[] difficultyLevels;

    private void Start()
    {
        // Retrieve selected difficulty and stage data
        int selectedDifficulty = PlayerPrefs.GetInt("SelectedDifficulty", 0);
        int selectedStage = PlayerPrefs.GetInt("SelectedStage", 0);

        // Set the background, customers, and difficulty based on the selected data
        if (selectedDifficulty < difficultyLevels.Length && selectedStage < customerSets.Length)
        {
            // Set the background
            backgrounds[selectedStage].SetActive(true);

            // Set the customers
            customerSets[selectedStage].SetActive(true);

            // Set the difficulty
            DifficultySelector selectedDiffLevel = difficultyLevels[selectedDifficulty];
            // You can implement logic to adjust game parameters based on the selected difficulty.
        }
        else
        {
            Debug.LogError("Invalid difficulty or stage selection!");
        }
    }
}
