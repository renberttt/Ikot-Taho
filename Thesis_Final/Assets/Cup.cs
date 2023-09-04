using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool isDragging = false;
    private Vector3 offset;

    private List<string> ingredients = new List<string>(); // List to track ingredients.

    public void AddIngredient(string ingredient)
    {
        // Check if the ingredient is not already in the list.
        if (!ingredients.Contains(ingredient))
        {
            // Add the ingredient to the list.
            ingredients.Add(ingredient);

            // Display a Debug log showing the placed ingredients.
            Debug.Log("Placed Ingredient: " + ingredient);

            // Perform any other actions or logic for the ingredient placement.
            // For example, you can update the cup's appearance or properties based on the ingredients.
        }
    }

    private void Start()
    {
        // Current Position of the CUP 
        initialPosition = transform.position;
    }

    // The cup would be able to be dragged 
    private void OnMouseDown()
    {
        if (!isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = transform.position - mousePosition;
            isDragging = true;
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;

            // Checks if the cup collides with the Trashbin and the cup would be removed if released on the trashcan
            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
            foreach (Collider2D collider in hitColliders)
            {
                if (collider.CompareTag("Trashbin"))
                {
                    Destroy(gameObject);
                    return;
                }
            }
            // The cup would return to the initial position when the cup is released
            transform.position = initialPosition;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
        }
    }
}
