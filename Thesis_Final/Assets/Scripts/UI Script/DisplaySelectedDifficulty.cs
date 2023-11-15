using UnityEngine;
using TMPro;
public class DisplaySelectedDifficulty : MonoBehaviour
{
    public TMP_Text difficultyDisplay; // Reference to TextMeshPro component for displaying difficulty

    void Start()
    {
        string difficulty = PlayerPrefs.GetString("Difficulty");
        difficultyDisplay.text = difficulty;
    }
}
