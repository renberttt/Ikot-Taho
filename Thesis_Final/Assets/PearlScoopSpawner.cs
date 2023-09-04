using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlScoopSpawner : MonoBehaviour
{
    public GameObject pearlScoopPrefab;
    private GameObject currentScoop;
    private bool isDragging = false;
    private Vector3 offset;

    private void OnMouseDown()
    {
        if (!isDragging)
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0f;

            currentScoop = Instantiate(pearlScoopPrefab, clickPosition, Quaternion.identity);
            offset = currentScoop.transform.position - clickPosition;
            isDragging = true;
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;

            // Check if the Pearl scoop is within the cup's collider.
            Collider2D cupCollider = GetComponent<Collider2D>();

            if (cupCollider != null && cupCollider.OverlapPoint(currentScoop.transform.position))
            {
                Debug.Log("Pearl scoop placed in cup.");

                // Access the Cup script of the cup GameObject.
                Cup cupScript = GetComponent<Cup>();

                if (cupScript != null)
                {
                    // Add "Pearls" to the cup's list of ingredients.
                    cupScript.AddIngredient("Pearls");
                }
            }

            Destroy(currentScoop);
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentScoop.transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, currentScoop.transform.position.z);
        }
    }
}
