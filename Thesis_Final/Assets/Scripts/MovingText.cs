using UnityEngine;
using TMPro;

public class MovingText : MonoBehaviour
{
    // The TMP text component
    public TextMeshProUGUI tmpText;

    // The target y-position
    public float targetY = 51f;

    // The speed at which the text should move
    public float moveSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure TMP text component is assigned
        if (tmpText == null)
        {
            Debug.LogError("TMP Text component is not assigned!");
            return;
        }

        // Move the TMP text upwards
        MoveTextUp();
    }

    // Method to move the TMP text upwards
    void MoveTextUp()
    {
        // Get the TMP text's RectTransform
        RectTransform textRectTransform = tmpText.rectTransform;

        // Calculate the target position with the specified y-coordinate
        Vector3 targetPosition = new Vector3(textRectTransform.position.x, targetY, textRectTransform.position.z);

        // Move towards the target position over time
        LeanTween.move(textRectTransform, targetPosition, moveSpeed)
            .setEase(LeanTweenType.easeOutQuad);
    }
}
