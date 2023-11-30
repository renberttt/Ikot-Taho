using UnityEngine;
using UnityEngine.UI;

public class Switcher : MonoBehaviour
{
    public GameObject[] backgrounds;
    public GameObject[] buttons;
    public GameObject[] texts;
    public GameObject continueButton;
    public Button previousButton;
    public Button nextButton;

    private int index = 0;

    void Start()
    {
        ShowBackgroundAtIndex(index);
    }

    void ShowBackgroundAtIndex(int newIndex)
    {
        newIndex = Mathf.Clamp(newIndex, 0, backgrounds.Length - 1);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(i == newIndex);
            buttons[i].SetActive(i == newIndex);
            texts[i].SetActive(i == newIndex);
        }

        index = newIndex;

        previousButton.interactable = (index > 0);
        nextButton.interactable = (index < backgrounds.Length - 1);
        continueButton.SetActive(!(index < backgrounds.Length - 1));
    }

    public void Next()
    {
        index = Mathf.Min(index + 1, backgrounds.Length - 1);
        ShowBackgroundAtIndex(index);
    }

    public void Previous()
    {
        index = Mathf.Max(index - 1, 0);
        ShowBackgroundAtIndex(index);
    }
}
