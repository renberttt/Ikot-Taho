using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customerPrefabs;
    public Sprite[] customerSprites;
    public float spawnInterval = 10f;
    public Transform customerContainer;

    private Queue<GameObject> customerQueue = new Queue<GameObject>();
    private float spawnTimer = 0f; // Timer to track the spawn interval

    private static bool isSpawningPaused;

    private void Update()
    {
        if (isSpawningPaused)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnCustomer();
            spawnTimer = 0f;
        }
    }

    private void SpawnCustomer()
    {
        GameObject customerPrefab = customerPrefabs[Random.Range(0, customerPrefabs.Length)];

        Vector3 spawnPosition = new Vector3(
            -480f, // X position
            220f,  // Y position
            0f     // Z position
        );

        GameObject newCustomer = Instantiate(customerPrefab, spawnPosition, Quaternion.identity);
        GameObject customer = GameObject.Find("Customer");
        Sprite selectedSprite = customerSprites[Random.Range(0, customerSprites.Length)];
        SpriteRenderer customerRenderer = newCustomer.GetComponent<SpriteRenderer>();

        if (customerRenderer != null)
        {
            customerRenderer.sprite = selectedSprite;
        }

        // Set the "Customer" GameObject as the parent/container
        newCustomer.transform.SetParent(customer.transform,false);
        customerQueue.Enqueue(newCustomer);
    }
    public void ServeCustomer()
    {
        if (customerQueue.Count > 0)
        {
            GameObject customerToServe = customerQueue.Dequeue(); // Dequeue the next customer.

            // Perform serving actions on customerToServe.

            // Destroy the customer or move them elsewhere when done serving.
            Destroy(customerToServe);
        }
    }
    // Method to toggle customer spawning pause
    public static void TogglePause(bool pause)
    {
        isSpawningPaused = pause;
    }
}