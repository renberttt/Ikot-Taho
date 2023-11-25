using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    public Button buttonToAnimate;
    public float animationDuration = 1f;
    public float animationDistance = 10f;

    private RectTransform buttonRectTransform;
    private Vector3 startPosition;

    void Start()
    {
        buttonRectTransform = buttonToAnimate.GetComponent<RectTransform>();
        startPosition = buttonRectTransform.anchoredPosition; // Use anchoredPosition instead of position

        // Start the animation when the script starts
        AnimateButton();
    }

    void AnimateButton()
    {
        // Animate the button's scale to create a pulse effect
        LeanTween.scale(buttonRectTransform, Vector3.one * 1.1f, animationDuration / 2)
            .setEaseOutQuad()
            .setOnComplete(() =>
            {
                LeanTween.scale(buttonRectTransform, Vector3.one, animationDuration / 2)
                    .setEaseInQuad()
                    .setOnComplete(() =>
                    {
                        // Start the animation again (looping)
                        AnimateButton();
                    });
            });
    }
}
