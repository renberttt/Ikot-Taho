using UnityEngine;

public class LastStageChecker : MonoBehaviour
{
    public GameObject continueBtn;
    public GameObject menuBtn;
    void Start()
    {
        int curentStage = PlayerPrefs.GetInt("SelectedStage");
        if(curentStage == 6)
        {
            continueBtn.SetActive(false);
            menuBtn.SetActive(true);
        }
        else
        {
            continueBtn.SetActive(true);
            menuBtn.SetActive(false);
        }
    }
}
