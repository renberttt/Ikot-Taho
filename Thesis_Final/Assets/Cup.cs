using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool isDragging = false;
    private Vector3 offset;

    private List<string> ingredients = new List<string>(); // List to track ingredients.
    public void AddIngredient(string ingredientName)
    {
        if (ingredients.Count < 4)
        {
            ingredients.Add(ingredientName);
            Debug.Log("Ingredients in the cup: ");
            foreach (string ingredient in ingredients)
            {
                Debug.Log(ingredient);
            }
        }
        else
        {
            Debug.Log("The cup is already full");
        }
    }
    private void Start()
    {
        initialPosition = transform.position;
    }

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
            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
            foreach (Collider2D collider in hitColliders)
            {
                if (collider.CompareTag("Trashbin"))
                {
                    Destroy(gameObject);
                    return;
                }
            }
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
