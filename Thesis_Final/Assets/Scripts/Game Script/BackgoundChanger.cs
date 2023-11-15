using UnityEngine;
using UnityEngine.UI;

public class BackgoundChanger : MonoBehaviour
{
    public Image displayImage;
    public Sprite[] images;

    void Start()
    {
        int selectedImageIndex = PlayerPrefs.GetInt("SelectedImageIndex", 0); // Get the selected index
        if (selectedImageIndex >= 0 && selectedImageIndex < images.Length)
        {
            displayImage.sprite = images[selectedImageIndex]; // Change the image based on the stored index
        }
    }
}
