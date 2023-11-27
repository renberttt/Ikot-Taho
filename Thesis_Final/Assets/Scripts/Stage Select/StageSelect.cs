// StageSelect.cs
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    public GameObject clacGameObject;
    public GameObject cthmGameObject;
    public GameObject ccjeGameObject;
    public GameObject ccedGameObject;
    public GameObject ceatGameObject;
    public GameObject cbaaGameObject;

    // Example boolean conditions
    public bool pandan;
    public bool chocolate;
    public bool mango;
    public bool ube;
    public bool tapioca;
    public bool strawberry;

    void Start()
    {
        // Deactivate the GameObjects by default
        clacGameObject.SetActive(false);
        cthmGameObject.SetActive(false);
        ccjeGameObject.SetActive(false);
        ccedGameObject.SetActive(false);
        ceatGameObject.SetActive(false);
        cbaaGameObject.SetActive(false);

        // Check if shop is clicked
        if (GameManager.Instance != null)
        {
            // Decide which GameObject to activate based on your game logic
            if (pandan && GameManager.Instance.shopClickedPandan)
            {
                clacGameObject.SetActive(true);
            }
            if (chocolate && GameManager.Instance.shopClickedChocolate)
            {
                cthmGameObject.SetActive(true);
            }
            if (mango && GameManager.Instance.shopClickedMango)
            {
                ccjeGameObject.SetActive(true);
            }
            if (ube && GameManager.Instance.shopClickedUbe)
            {
                ccedGameObject.SetActive(true);
            }
            if (tapioca && GameManager.Instance.shopClickedTapioca)
            {
                ceatGameObject.SetActive(true);
            }
            if (strawberry && GameManager.Instance.shopClickedStrawberry)
            {
                cbaaGameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("GameManager instance is null!");
        }
    }
}
