using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customerPrefabs; // Array of customer prefabs
    public float spawnInterval = 3f; // Interval in seconds between customer spawns
    public float[] spawnPositions; // Array of spawn positions

    private List<GameObject> spawnedCustomers = new List<GameObject>(); // List to store spawned customers
    private float spawnTimer = 0f; // Timer to track the spawn interval

    private static bool isSpawningPaused;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnCustomer();
            spawnTimer = 0f;
        }
        
           if (isSpawningPaused)
            return;

    }

    private void SpawnCustomer()
    {
        // Randomly choose one of the customer prefabs
        GameObject customerPrefab = customerPrefabs[Random.Range(0, customerPrefabs.Length)];

        // Check if all spawn positions are occupied
        bool areAllPositionsOccupied = true;
        foreach (float positionX in spawnPositions)
        {
            if (!IsPositionOccupied(positionX))
            {
                areAllPositionsOccupied = false;
                break;
            }
        }

        // If all positions are occupied, return without spawning a customer
        if (areAllPositionsOccupied)
            return;

        // Randomly choose one of the unoccupied spawn positions
        float spawnX = GetRandomUnoccupiedPosition();

        // Instantiate the customer prefab at the spawn position
        GameObject customer = Instantiate(customerPrefab, new Vector3(spawnX, transform.position.y, transform.position.z), Quaternion.identity);
        spawnedCustomers.Add(customer);

        // Randomly choose a position from the stopPositions array for the customer to stop
        int stopPositionIndex = Random.Range(0, customer.GetComponent<CustomerMovement>().stopPositions.Length);
        customer.GetComponent<CustomerMovement>().currentStopIndex = stopPositionIndex;
    }

    private bool IsPositionOccupied(float positionX)
    {
        // Check if the spawn position is occupied
        foreach (GameObject customer in spawnedCustomers)
        {
            if (customer != null && Mathf.Approximately(customer.transform.position.x, positionX))
            {
                return true; // Position is occupied by another customer
            }
        }

        return false; // Position is not occupied
    }

    private float GetRandomUnoccupiedPosition()
    {
        // Create a list of unoccupied positions
        List<float> unoccupiedPositions = new List<float>();
        foreach (float positionX in spawnPositions)
        {
            if (!IsPositionOccupied(positionX))
            {
                unoccupiedPositions.Add(positionX);
            }
        }

        // Return a random position from the unoccupied positions list
        return unoccupiedPositions[Random.Range(0, unoccupiedPositions.Count)];
    }

    public static void TogglePause(bool pause)
    {
        isSpawningPaused = pause;
    }
}
