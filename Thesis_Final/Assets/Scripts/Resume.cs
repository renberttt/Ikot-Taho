using UnityEngine;
using UnityEngine.SceneManagement;

public class Resume : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Restart the current scene

        Destroy(GameObject.FindWithTag("Pauses"));

        // Resume the game by setting the time scale to 1
        Time.timeScale = 1f;
    }
}