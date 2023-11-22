using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public GameObject heartPrefab;
    private GameObject[] heartObjects;
    public Text healthText;

    private int maxHealth;
    private int currentHealth;

    // Array to store the original colors of the hearts
    private Color[] originalColors;

    private void Awake()
    {
        maxHealth = 1; // Set the maximum health
        currentHealth = maxHealth;

        // Initialize heart objects array
        heartObjects = new GameObject[maxHealth];
        originalColors = new Color[maxHealth];

        // Set the initial position for heart objects
        Vector3 initialPosition = transform.position - Vector3.up * 0.5f; // Lower the initial position
        float spacing = 0.8f; // Adjust this value based on your preference

        // Instantiate heart objects and set their positions
        for (int i = 0; i < maxHealth; i++)
        {
            heartObjects[i] = Instantiate(heartPrefab, initialPosition + Vector3.right * spacing * i, Quaternion.identity, transform);
            heartObjects[i].SetActive(true); // Ensure they are initially active

            // Get the original color of the heart object
            Image heartImage = heartObjects[i].GetComponent<Image>();
            if (heartImage != null)
            {
                originalColors[i] = heartImage.color;
            }
        }

        // Debug log to check if heartObjects array is properly initialized
        Debug.Log($"Number of heart objects: {heartObjects.Length}");

        UpdateHealthDisplay();
    }

    private void UpdateHealthDisplay()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
        else
        {
            Debug.LogWarning("No Text component found for the health bar.");
        }

        // Update the appearance of heart objects based on current health
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < currentHealth)
            {
                // Change the color of the heart object when it's visible
                Image heartImage = heartObjects[i].GetComponent<Image>();
                if (heartImage != null)
                {
                    // Set the color to black based on health percentage
                    float healthPercentage = (float)currentHealth / maxHealth;
                    heartImage.color = Color.Lerp(originalColors[i], Color.black, 1 - healthPercentage);
                }

                heartObjects[i].SetActive(true);
            }
            else
            {
                heartObjects[i].SetActive(false);
            }
        }
    }

    public void DecrementHealth()
    {
        currentHealth--;

        // Ensure health doesn't go below 0
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log("-1 HP");

        UpdateHealthDisplay();
    }
}
