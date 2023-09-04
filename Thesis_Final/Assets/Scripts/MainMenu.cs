using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Main Menu");
    }
}