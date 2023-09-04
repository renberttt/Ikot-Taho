using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartingScreen : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public float progressDelay = 0.5f; // Delay time between each progress update

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        loadingScreen.SetActive(true);
        float targetProgress = 0.9f; // The target progress value where the delay is introduced
        float currentProgress = 0f; // Current progress value

        while (currentProgress < targetProgress)
        {
            currentProgress = Mathf.Clamp01(operation.progress / targetProgress);
            slider.value = currentProgress;

            yield return new WaitForSeconds(progressDelay); // Delay between each progress update
        }

        yield return new WaitForSeconds(1f); // Additional delay before scene activation

        operation.allowSceneActivation = true;
        loadingScreen.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
