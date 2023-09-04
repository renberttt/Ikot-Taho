using UnityEngine;

public class PauseButton : MonoBehaviour
{

    public GameObject pause;
    public static bool IsPaused { get; private set; }

    private void OnMouseDown()
    {
        TogglePause();
      Instantiate(pause, new Vector3(0f, 0f, 0f), Quaternion.identity);

    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;

        // Trigger pause or resume functionality in other scripts
        Time.timeScale = IsPaused ? 0f : 1f;
        CustomerSpawner.TogglePause(IsPaused);
        CustomerMovement.TogglePause(IsPaused);
    }
}
