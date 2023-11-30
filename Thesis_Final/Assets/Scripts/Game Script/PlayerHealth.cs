using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public FadeTransition fadeTransition;
    public GameObject[] heartImages;
    public GameObject loseGameObject;
    public Sprite emptyHeart;
    private int health = 5;

    private void Start()
    {
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
            loseGameObject.SetActive(true);
            fadeTransition.TogglePause();
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
