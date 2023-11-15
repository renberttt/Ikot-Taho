using UnityEngine;
public class MainGameController : MonoBehaviour
{
    private static MainGameController instance;
    private string selectedStage;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ResetDifficulty()
    {
        PlayerPrefs.DeleteKey("Difficulty");
    }
    public void SetDifficulty(string difficulty)
    {
        PlayerPrefs.SetString("Difficulty", difficulty);
    }
    public void SetSelectedStage(string stage)
    {
        selectedStage = stage;
    }
    public string GetSelectedStage()
    {
        return selectedStage;
    }
}
