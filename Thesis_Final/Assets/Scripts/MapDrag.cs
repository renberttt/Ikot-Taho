using UnityEngine;

public class DragMap : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Rigidbody2D rb;

    private Camera mainCamera;
    private Vector2 cameraMin;
    private Vector2 cameraMax;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        mainCamera = Camera.main;
        CalculateCameraBounds();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 touchEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchDelta = touchEndPos - touchStartPos; // Inverted direction

            Vector2 newPosition = (Vector2)transform.position + touchDelta;

            newPosition.x = Mathf.Clamp(newPosition.x, cameraMin.x, cameraMax.x);
            newPosition.y = Mathf.Clamp(newPosition.y, cameraMin.y, cameraMax.y);

            rb.MovePosition(newPosition);
            touchStartPos = touchEndPos;
        }
    }

    private void CalculateCameraBounds()
    {
        float cameraHeight = mainCamera.orthographicSize / .8f;
        float cameraWidth = cameraHeight * mainCamera.aspect / .9f;

        cameraMin = new Vector2(mainCamera.transform.position.x - cameraWidth, mainCamera.transform.position.y - cameraHeight);
        cameraMax = new Vector2(mainCamera.transform.position.x + cameraWidth, mainCamera.transform.position.y + cameraHeight);
    }
}
