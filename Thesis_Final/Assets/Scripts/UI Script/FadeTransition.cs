using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeTransition : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public static bool IsPaused { get; private set; }
    private int currentStage;

    public void LoadNextLevel(int sceneIndex)
    {
        if (!gameObject.activeInHierarchy) return; // Check if the script's GameObject is active

        if (IsPaused)
        {
            Resume();
        }

        StartCoroutine(LoadLevel(sceneIndex));
        CustomerMovement.ResetOccupiedPositions();
    }
    public void ContinueLevel()
    {
        int currentStage = PlayerPrefs.GetInt("SelectedStage");
        currentStage++;
        if(currentStage >= 7)
        {
            currentStage = 0;
        }
        PlayerPrefs.SetInt("SelectedStage", currentStage);
        Restart();
    }

    IEnumerator LoadLevel(int sceneIndex){
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneIndex);
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        IsPaused = false;
    }
    public void TogglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0f : 1f;
    }
    public void Restart()
    {
        if (IsPaused)
        {
            TogglePause();
        }
        CustomerMovement.ResetOccupiedPositions();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}