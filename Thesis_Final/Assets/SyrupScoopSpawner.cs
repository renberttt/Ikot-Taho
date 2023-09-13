using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyrupScoopSpawner : MonoBehaviour
{
    public GameObject syrupScoopPrefab;
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

            currentScoop = Instantiate(syrupScoopPrefab, clickPosition, Quaternion.identity);
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
