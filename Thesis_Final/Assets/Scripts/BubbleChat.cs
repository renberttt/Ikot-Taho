using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleChat : MonoBehaviour
{
    private float displayDuration; // Duration in seconds to display the bubble chat

    public void Initialize(float duration)
    {
        displayDuration = duration;
        StartCoroutine(DisplayBubbleChat());
    }

    private IEnumerator DisplayBubbleChat()
    {
        yield return new WaitForSeconds(displayDuration);
        Destroy(gameObject);
    }
}
