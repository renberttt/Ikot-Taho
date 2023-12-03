using UnityEngine;

public class LastStageChecker : MonoBehaviour
{
    public GameObject continueBtn;
    public GameObject menuBtn;
    public GameObject shopBtn;

    void Start()
    {
        int currentStage = PlayerPrefs.GetInt("SelectedStage");

        bool purchased = false;
        int requiredItemIndex = currentStage;

        if (PlayerPrefs.GetInt("ShopItem_" + requiredItemIndex, 0) == 1)
        {
            purchased = true;
        }

        if (currentStage == 6)
        {
            continueBtn.SetActive(false);
            menuBtn.SetActive(true);
            shopBtn.SetActive(false);
        }
        else if (currentStage < 6 && !purchased)
        {
            continueBtn.SetActive(false);
            shopBtn.SetActive(true);
            menuBtn.SetActive(false);
        }
        else if (currentStage < 6 && purchased)
        {
            continueBtn.SetActive(true);
            shopBtn.SetActive(false);
            menuBtn.SetActive(false);
        }
    }
}