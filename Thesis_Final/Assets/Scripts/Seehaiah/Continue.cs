using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    // Set the target scene name in the Inspector
    public string targetScene = "Shop";

    void Start()
    {
        // Get the Button component on the GameObject this script is attached to
        Button button = GetComponent<Button>();

        // Add a listener to the button that triggers the SwitchScene method when clicked
        button.onClick.AddListener(SwitchScene);
    }

    void SwitchScene()
    {
        // Load the target scene
        SceneManager.LoadScene(targetScene);
    }
}
