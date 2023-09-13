using UnityEngine;

public class AutoAdjustCamera : MonoBehaviour
{
    public int targetWidth = 16; // Set your target width here.
    public int targetHeight = 9; // Set your target height here.

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("No main camera found. Make sure you have a camera tagged as 'MainCamera'.");
            return;
        }

        AdjustCamera();
    }

    void Update()
    {
        // You can call AdjustCamera in Update if you want it to adapt dynamically.
        // AdjustCamera();
    }

    void AdjustCamera()
    {
        float targetAspect = (float)targetWidth / targetHeight;
        float currentAspect = (float)Screen.width / Screen.height;

        float orthoSize = mainCamera.orthographicSize;

        if (currentAspect >= targetAspect)
        {
            orthoSize = targetHeight / 2f;
        }
        else
        {
            orthoSize = targetWidth / 2f / currentAspect;
        }

        mainCamera.orthographicSize = orthoSize;
    }
}
