using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject[] heartImages;
    public Sprite emptyHeart;
    private int health = 5;

    private void UpdateHearts()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            SpriteRenderer heartRenderer = heartImages[health].GetComponent<SpriteRenderer>();
            heartRenderer.sprite = emptyHeart;
        }
    }

    public void DecrementHealth()
    {
        health--;
        UpdateHearts();
    }
}
