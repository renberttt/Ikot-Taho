using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuToMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Set the GameObject as inactive initially
        gameObject.SetActive(false);

        // Invoke the method to make the object visible after 7 seconds
        Invoke("EnableObject", 12f);
    }

    // Method to enable the GameObject
    void EnableObject()
    {
        // Activate the GameObject, making it visible
        gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        // Load the "Main Menu" scene
        SceneManager.LoadScene("Main Menu");
    }
}
