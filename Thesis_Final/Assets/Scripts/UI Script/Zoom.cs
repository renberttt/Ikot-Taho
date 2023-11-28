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
        if (Input.touchCount == 2)
        {

            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            float newOrthoSize = mainCamera.orthographicSize + deltaMagnitudeDiff * zoomSpeed;
            newOrthoSize = Mathf.Clamp(newOrthoSize, minZoom, maxZoom);
            mainCamera.orthographicSize = newOrthoSize;
        }
    }
}
