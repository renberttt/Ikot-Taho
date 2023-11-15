using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customerPrefabs;
    public Sprite[] customerSprites;
    public float spawnInterval = 10f;
    public Transform customerContainer;

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
            -1f    // Z position (modified to be -1)
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
        newCustomer.transform.SetParent(customer.transform, false);
    }
    // Method to toggle customer spawning pause
    public static void TogglePause(bool pause)
    {
        isSpawningPaused = pause;
    }
}