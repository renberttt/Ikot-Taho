using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool shopClickedPandan;
    public bool shopClickedChocolate;
    public bool shopClickedMango;
    public bool shopClickedUbe;
    public bool shopClickedTapioca;
    public bool shopClickedStrawberry;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
