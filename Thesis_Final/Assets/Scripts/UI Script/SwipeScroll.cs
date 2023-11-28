using UnityEngine;
using UnityEngine.UI;

public class SwipeScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float swipeSpeed = 5.0f;
    public float snapSpeed = 10.0f;
    public float itemSpacing = 200.0f;

    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private Vector2 swipeDelta;

    private RectTransform contentRect;
    private Vector2 contentStartPosition;
    private Vector2 contentEndPosition;
    private int currentItemIndex;

    void Start()
    {
        contentRect = scrollRect.content.GetComponent<RectTransform>();
        contentStartPosition = contentRect.anchoredPosition;
        contentEndPosition = contentStartPosition;
        currentItemIndex = 0;
    }

    void Update()
    {
        HandleSwipe();
    }

    void HandleSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
            currentTouchPosition = startTouchPosition;
        }

        if (Input.GetMouseButton(0))
        {
            currentTouchPosition = Input.mousePosition;
            swipeDelta = currentTouchPosition - startTouchPosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            float normalizedSwipeDelta = swipeDelta.x / Screen.width;

            if (Mathf.Abs(normalizedSwipeDelta) > 0.1f)
            {
                int direction = Mathf.Clamp(Mathf.RoundToInt(normalizedSwipeDelta), -1, 1);
                currentItemIndex -= direction;

                float destination = contentStartPosition.x - currentItemIndex * itemSpacing;
                contentEndPosition = new Vector2(destination, contentRect.anchoredPosition.y);
            }
        }

        contentRect.anchoredPosition = Vector2.Lerp(contentRect.anchoredPosition, contentEndPosition, Time.deltaTime * snapSpeed);
    }
}
