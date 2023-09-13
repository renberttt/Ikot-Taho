using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour
{
    public Animator transition;
    
    public float transitionTime = 1f;
    public static bool IsPaused { get; private set; }

    public void LoadNextLevel(int sceneIndex)
    {
        StartCoroutine(LoadLevel(sceneIndex));
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
        Destroy(GameObject.FindWithTag("Pauses"));
        Time.timeScale = 1f;
        IsPaused = false;
    }
    public void TogglePause()
    {
        IsPaused = !IsPaused;

        // Trigger pause or resume functionality in other scripts
        Time.timeScale = IsPaused ? 0f : 1f;
        CustomerSpawner.TogglePause(IsPaused);
        CustomerMovement.TogglePause(IsPaused);
    }
    public void Restart()
    {
        // Unpause the game if it was paused.
        if (IsPaused)
        {
            TogglePause();
        }

        // Restart the current scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
