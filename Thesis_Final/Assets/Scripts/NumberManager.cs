using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberManager : MonoBehaviour
{
    public int maxHealth = 5; // Maximum health value
    public Text numberText; // Reference to the Text UI component

    private int currentHealth; // Current health value

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maximum health
        UpdateNumberText(); // Update the number text on start
    }

    public void DecreaseNumber()
    {
        currentHealth--; // Decrease the current health value
        UpdateNumberText(); // Update the number text

        if (currentHealth <= 0)
        {
            // Handle game over or any other actions when health reaches zero
        }
    }

    private void UpdateNumberText()
    {
        numberText.text = currentHealth.ToString(); // Update the number text with the current health value
    }
}
