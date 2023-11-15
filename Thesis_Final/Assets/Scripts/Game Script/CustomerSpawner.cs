using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public Sprite[] cscsCustomer;
    public Sprite[] clacCustomer;
    public Sprite[] cthmCustomer;
    public Sprite[] cbaaCustomer;
    public Sprite[] ccjeCustomer;
    public Sprite[] ceatCustomer;
    public Sprite[] coedCustomer;
    public float spawnInterval = 10f;
    public Transform customerContainer;

    private float spawnTimer = 0f; // Timer to track the spawn interval
    private Sprite[] selectedCustomerSet;
    private static bool isSpawningPaused;

    private void Start()
    {
        int selectedImageIndex = PlayerPrefs.GetInt("SelectedImageIndex", 0);
        Debug.Log(PlayerPrefs.GetInt("SelectedImageIndex", 0));

        switch (selectedImageIndex)
        {
            case 0:
                selectedCustomerSet = cscsCustomer;
                break;
            case 1:
                selectedCustomerSet = clacCustomer;
                break;
            case 2:
                selectedCustomerSet = cthmCustomer;
                break;
            case 3:
                selectedCustomerSet = cbaaCustomer;
                break;
            case 4:
                selectedCustomerSet = ccjeCustomer;
                break;
            case 5:
                selectedCustomerSet = ceatCustomer;
                break;
            case 6:
                selectedCustomerSet = coedCustomer;
                break;
        }
    }
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
        Vector3 spawnPosition = new Vector3(
            -480f, // X position
            220f,  // Y position
            -1f    // Z position (modified to be -1)
        );

        GameObject newCustomer = Instantiate(customerPrefab, spawnPosition, Quaternion.identity);
        GameObject customer = GameObject.Find("Customer");
        Sprite selectedSprite = selectedCustomerSet[Random.Range(0, selectedCustomerSet.Length)];
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