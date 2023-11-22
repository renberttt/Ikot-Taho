using UnityEngine;
using UnityEngine.UI;

public class ButtonImageDisplay : MonoBehaviour
{
    public Image displayImage;
    public Sprite[] images;  
    public void OnButtonClick(Button button)
    {
        int index = System.Array.IndexOf(GetComponentsInChildren<Button>(), button);

        if (index >= 0 && index < images.Length)
        {
            displayImage.sprite = images[index];
            PlayerPrefs.SetInt("SelectedStage", index);
        }
    }
}