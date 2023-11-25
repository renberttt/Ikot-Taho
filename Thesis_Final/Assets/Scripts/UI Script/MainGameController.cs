using UnityEngine;
public class MainGameController : MonoBehaviour
{
    public void ResetDifficulty()
    {
        PlayerPrefs.DeleteKey("Difficulty");
    }
    public void SetDifficulty(string difficulty)
    {
        PlayerPrefs.SetString("Difficulty", difficulty);
    }
}
