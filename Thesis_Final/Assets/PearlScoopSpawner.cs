using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlScoopSpawner : MonoBehaviour
{
    public GameObject pearlScoopPrefab;
    private GameObject currentScoop;
    private bool canInteractWithScoop = true;
    private bool isDragging = false;
    private Vector3 offset;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing.");
        }
    }
    private void OnMouseDown()
    {
        if (canInteractWithScoop)
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0f;

            currentScoop = Instantiate(pearlScoopPrefab, clickPosition, Quaternion.identity);
            canInteractWithScoop = false;
            offset = currentScoop.transform.position - clickPosition;
            isDragging = true;

            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(currentScoop.transform.position, currentScoop.GetComponent<BoxCollider2D>().size, 0f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Cup"))
                {
                    Destroy(currentScoop);
                    Cup cup = collider.GetComponent<Cup>();
                    if (cup != null)
                    {
                        cup.AddIngredient("Pearl");
                    }
                    break;
                }
            }
            Destroy(currentScoop);

            canInteractWithScoop = true;
            isDragging = false;
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
