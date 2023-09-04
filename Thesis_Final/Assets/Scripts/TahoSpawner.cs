using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TahoSpawner : MonoBehaviour
{
    public GameObject TahoIngredients;
    public Collider2D spawnArea; // Collider representing the valid spawning area

    private GameObject currentTaho;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnTaho();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDragging();
        }
    }

    private void SpawnTaho()
    {
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPosition.z = 0.0f; // Set the Z position to 0 to ensure it's on the same plane

        if (spawnArea.OverlapPoint(spawnPosition)) // Check if spawn position is within the spawn area
        {
            currentTaho = Instantiate(TahoIngredients, spawnPosition, transform.rotation);
            currentTaho.GetComponent<SoyaScoop>().enabled = true;
            currentTaho.transform.position += Vector3.up * 0.1f; // Adjust the position slightly to avoid stacking

            currentTaho.GetComponent<SoyaScoop>().OnMouseDown(); // Trigger the OnMouseDown event of the spawned object
        }
    }

    private void StopDragging()
    {
        if (currentTaho != null)
        {
            currentTaho.GetComponent<SoyaScoop>().OnMouseUp(); // Trigger the OnMouseUp event of the spawned object
            currentTaho = null;
        }
    }
}
    