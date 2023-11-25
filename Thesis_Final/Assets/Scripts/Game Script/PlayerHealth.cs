using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public FadeTransition fadeTransition;
    public GameObject[] heartImages;
    public GameObject loseGameObject;
    public Sprite emptyHeart;
    private int health = 5;
    private void UpdateHearts()
    {

        for (int i = 0; i < heartImages.Length; i++)
        {
            SpriteRenderer heartRenderer = heartImages[health].GetComponent<SpriteRenderer>();
            heartRenderer.sprite = emptyHeart;

            if(health == 0)
            {
                loseGameObject.SetActive(true);
                fadeTransition.TogglePause();
            }
        }
    }

    public void DecrementHealth()
    {
        health--;
        UpdateHearts();
    }
}
