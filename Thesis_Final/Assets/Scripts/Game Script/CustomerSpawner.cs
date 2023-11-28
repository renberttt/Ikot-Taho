using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;

    public Sprite[] cscsCustomer;
    public Sprite[] clacCustomer;
    public Sprite[] cthmCustomer;
    public Sprite[] ccjeCustomer;
    public Sprite[] coedCustomer;
    public Sprite[] ceatCustomer;
    public Sprite[] cbaaCustomer;

    private Sprite[] selectedCustomerSet;
    public Transform customerContainer;

    private float spawnInterval = 5f;
    private float spawnTimer;
    private static bool isSpawningPaused;
    private void Start()
    {
        SetSelectedCustomerSet();
        string difficulty = PlayerPrefs.GetString("Difficulty");
        switch (difficulty)
        {
            case "Easy":
                spawnInterval = 5f;
                break;
            case "Medium":
                spawnInterval = 5f;
                break;
            case "Hard":
                spawnInterval = 2f;
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
    public void SetSpawnInterval(float interval)
    {
        spawnInterval = interval;
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
    private void SetSelectedCustomerSet()
    {
        int selectedImageIndex = PlayerPrefs.GetInt("SelectedStage", 0);

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
                selectedCustomerSet = ccjeCustomer;
                break;
            case 4:
                selectedCustomerSet = coedCustomer;
                break;
            case 5:
                selectedCustomerSet = ceatCustomer;
                break;
            case 6:
                selectedCustomerSet = cbaaCustomer;
                break;
        }
    }
    public static void TogglePause(bool pause)
    {
        isSpawningPaused = pause;
    }
}