using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlSpawner : MonoBehaviour
{
    public GameObject pearlScoop;
    public Collider2D spawnArea; // Collider representing the valid spawning area

    private GameObject currentpearl;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnPearl();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDragging();
        }
    }

    private void SpawnPearl()
    {
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPosition.z = 0.0f; // Set the Z position to 0 to ensure it's on the same plane

        if (spawnArea.OverlapPoint(spawnPosition)) // Check if spawn position is within the spawn area
        {
            currentpearl = Instantiate(pearlScoop, spawnPosition, transform.rotation);
            //currentpearl.GetComponent<PearlScoop>().enabled = true;
            currentpearl.transform.position += Vector3.up * 0.1f; // Adjust the position slightly to avoid stacking

           // currentpearl.GetComponent<PearlScoop>().OnMouseDown(); // Trigger the OnMouseDown event of the spawned object
        }
    }

    private void StopDragging()
    {
        if (currentpearl != null)
        {
            //currentpearl.GetComponent<PearlScoop>().OnMouseUp(); // Trigger the OnMouseUp event of the spawned object
            currentpearl = null;
        }
    }
}
