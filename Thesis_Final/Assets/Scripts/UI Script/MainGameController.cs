using UnityEngine;
public class MainGameController : MonoBehaviour
{
    public int playerLost;
    public void ResetDifficulty()
    {
        PlayerPrefs.DeleteKey("Difficulty");
    }
    public void SetDifficulty(string difficulty)
    {
        PlayerPrefs.SetString("Difficulty", difficulty);
    }
    public void GetPlayerStatus(int playerLost)
    {
        PlayerPrefs.SetInt("PlayerStatus", playerLost);
    }
}
