using UnityEngine;
using UnityEngine.UI;

public class BackgoundChanger : MonoBehaviour
{
    public Image displayImage;
    public GameObject tahoImage;
    public GameObject pearlImage;
    public GameObject syrupImage;
    public Sprite[] backgroundImages;
    public Sprite[] tahoSpriteImages;
    public Sprite[] pearlSpriteImages;
    public Sprite[] syrupSpriteImages;
    void Start()
    {
        int selectedImageIndex = PlayerPrefs.GetInt("SelectedStage", 0);
        SpriteRenderer tahoimage = tahoImage.GetComponent<SpriteRenderer>();
        SpriteRenderer pearimage = pearlImage.GetComponent<SpriteRenderer>();
        SpriteRenderer syrupimage = syrupImage.GetComponent<SpriteRenderer>();

        if (selectedImageIndex >= 0 && selectedImageIndex < backgroundImages.Length)
        {
            displayImage.sprite = backgroundImages[selectedImageIndex];
            tahoimage.sprite = tahoSpriteImages[selectedImageIndex];
            pearimage.sprite = pearlSpriteImages[selectedImageIndex];
            syrupimage.sprite = syrupSpriteImages[selectedImageIndex];
        }
    }
}
