using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoyaScoopSpawner : MonoBehaviour
{
    public GameObject tahoScoopPrefab;
    private GameObject currentScoop;
    private bool canInteractWithScoop = true;
    private bool isDragging = false;
    private Vector3 offset;

    private void OnMouseDown()
    {
        if (canInteractWithScoop)
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0f;

            currentScoop = Instantiate(tahoScoopPrefab, clickPosition, Quaternion.identity);
            canInteractWithScoop = false;
            offset = currentScoop.transform.position - clickPosition;
            isDragging = true;
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
    }}
