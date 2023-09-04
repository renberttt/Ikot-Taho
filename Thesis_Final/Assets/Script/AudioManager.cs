using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip buttonSelect;
    public AudioClip winSound;
    public AudioClip syrupScoop;
    public AudioClip sagoScoop;
    public AudioClip tahoScoop;
    public AudioClip gameOver;

    public static AudioManager instance;

    public void Awake()
    {
        if (instance == null) {
        instance = this;
        DontDestroyOnLoad(gameObject);
 
    } else if (instance != this) {
        Destroy(instance.gameObject);
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
        
    }

    public void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
