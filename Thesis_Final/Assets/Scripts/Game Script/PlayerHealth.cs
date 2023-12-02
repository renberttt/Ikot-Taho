using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public FadeTransition fadeTransition;
    private StageSelect stageSelect;
    public GameObject[] heartImages;
    public GameObject loseGameObject;
    public GameObject warningGameObject;
    public Sprite emptyHeart;
    private int health = 5;
    private bool isPlayerLost;

    private void Start()
    {
        stageSelect = FindObjectOfType<StageSelect>();
        string difficulty = PlayerPrefs.GetString("Difficulty");
        switch (difficulty)
        {
            case "Easy":
                DisableHearts();
                break;
            case "Medium":
                EnableHearts();
                break;
            case "Hard":
                EnableHearts();
                break;
        }
    }

    private void EnableHearts()
    {
        foreach (var heart in heartImages)
        {
            heart.SetActive(true);
        }
    }

    private void DisableHearts()
    {
        foreach (var heart in heartImages)
        {
            heart.SetActive(false);
        }
    }

    private void UpdateHearts()
    {
        if (health <= 0 && AreHeartsActive())
        {
            isPlayerLost = true;
            PlayerPrefs.SetInt("PlayerLost", isPlayerLost ? 1 : 0);
            int lossesCount = PlayerPrefs.GetInt("LossesCount", 0);
            lossesCount++;
            PlayerPrefs.SetInt("LossesCount", lossesCount);

            if(lossesCount >= 3)
            {
                warningGameObject.SetActive(true);
                fadeTransition.TogglePause();
            }
            else
            {
                loseGameObject.SetActive(true);
                fadeTransition.TogglePause();
            } 
        }

        for (int i = 0; i < heartImages.Length; i++)
        {
            SpriteRenderer heartRenderer = heartImages[health].GetComponent<SpriteRenderer>();
            heartRenderer.sprite = (i < health) ? null : emptyHeart;
        }
    }

    private bool AreHeartsActive()
    {
        foreach (var heart in heartImages)
        {
            if (heart.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    public void DecrementHealth()
    {
        health--;
        UpdateHearts();
    }
}
