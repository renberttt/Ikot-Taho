using UnityEngine;

public class Zoom : MonoBehaviour
{
    public float minZoom = 3f;
    public float maxZoom = 5f;
    public float zoomSpeed = 1f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Check for two-finger touch
        if (Input.touchCount == 2)
        {
            // Get the positions of both touches
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Calculate the initial and current distance between the two touches
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Calculate the difference in magnitudes
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // Adjust the camera's orthographic size (zoom level)
            float newOrthoSize = mainCamera.orthographicSize + deltaMagnitudeDiff * zoomSpeed;

            // Clamp the zoom level to minZoom and maxZoom
            newOrthoSize = Mathf.Clamp(newOrthoSize, minZoom, maxZoom);

            // Apply the new orthographic size to the camera
            mainCamera.orthographicSize = newOrthoSize;
        }
    }
}
