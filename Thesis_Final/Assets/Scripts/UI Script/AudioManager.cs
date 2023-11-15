using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip backgroundGame;
    public AudioClip buttonSelect;
    public AudioClip winSound;
    public AudioClip syrupScoop;
    public AudioClip sagoScoop;
    public AudioClip tahoScoop;
    public AudioClip gameOver;

    public static AudioManager instance;

    private Dictionary<string, AudioClip> sceneBackgroundMusic = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        sceneBackgroundMusic["Main Menu"] = background;
        sceneBackgroundMusic["Stage Selection"] = background;
        sceneBackgroundMusic["Difficulty Selection"] = background;
        sceneBackgroundMusic["Shop"] = background;
        sceneBackgroundMusic["Game"] = backgroundGame;

        int sceneCount = SceneManager.sceneCount;
        for (int i = 0; i < sceneCount; i++)
        {
            string sceneName = SceneManager.GetSceneAt(i).name;

            if (sceneBackgroundMusic.ContainsKey(sceneName))
            {
                musicSource.clip = sceneBackgroundMusic[sceneName];
                musicSource.Play();
                break;
            }
        }
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
