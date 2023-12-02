using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    public Button[] buttonToAnimate;
    public float animationDuration = 1f;
    public float animationDistance = 10f;

    private RectTransform buttonRectTransform;
    private Vector3 startPosition;

    void Start()
    {
        foreach (Button button in buttonToAnimate)
        {
            if (button != null && button.interactable)
            {
                buttonRectTransform = button.GetComponent<RectTransform>();
                startPosition = buttonRectTransform.anchoredPosition;
                AnimateButton(buttonRectTransform);
            }
        }
    }

    void AnimateButton(RectTransform buttonRectTransform)
    {
        LeanTween.scale(buttonRectTransform, Vector3.one * 1.1f, animationDuration / 2)
            .setEaseOutQuad()
            .setOnComplete(() =>
            {
                LeanTween.scale(buttonRectTransform, Vector3.one, animationDuration / 2)
                    .setEaseInQuad()
                    .setOnComplete(() =>
                    {
                        AnimateButton(buttonRectTransform);
                    });
            });
    }
}
