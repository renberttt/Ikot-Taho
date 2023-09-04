using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public float[] stopPositions; // Array of stop positions
    public float returnXPosition; // X position to which the customer will return when colliding with FinalTaho
    public float returnDelay; // Delay in seconds before returning if FinalTaho is not colliding
    public GameObject[] bubbleChatPrefabs; // Array of bubble chat prefabs
    public Sprite sprite1; // First sprite
    public Sprite sprite2; // Second sprite

    public bool isMoving = true; // Flag indicating if the customer is currently moving
    public bool isReturning = false; // Flag indicating if the customer is currently returning
    public int currentStopIndex = 0; // Index of the current stop position
    private Vector3 startPosition; // Starting position for the return movement
    private bool isTrackingReturnTimer = false; // Flag indicating if the return timer should be tracked
    private float returnTimer = 0f; // Timer to track the delay before returning
    private GameObject currentBubbleChat; // Reference to the current bubble chat instance
    private Collider2D customerCollider; // Reference to the customer's collider component
    private SpriteRenderer spriteRenderer; // Reference to the sprite renderer component

    private static bool isMovementPaused;


    private void Start()
    {
        startPosition = transform.position;
        customerCollider = GetComponent<Collider2D>(); // Get the collider component
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the sprite renderer component

        // Randomly choose one of the stop positions
        currentStopIndex = Random.Range(0, stopPositions.Length);

        // Randomly assign a sprite
        int spriteIndex = Random.Range(0, 2);
        spriteRenderer.sprite = spriteIndex == 0 ? sprite1 : sprite2;
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveToStopPosition();
        }
        else if (isReturning)
        {
            MoveToReturnPosition();
        }
        else if (isTrackingReturnTimer)
        {
            UpdateReturnTimer();
        }

        if (isMovementPaused)
            return;
    }

    private void MoveToStopPosition()
    {
        // Move the customer towards the current stop position
        float targetX = stopPositions[currentStopIndex];
        float step = speed * Time.deltaTime;

        // Check if the target position is occupied
        bool isTargetOccupied = IsPositionOccupied(targetX);

        if (!isTargetOccupied)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), step);

            // Check if the customer has reached the stop position
            if (Mathf.Approximately(transform.position.x, targetX))
            {
                isMoving = false;
                ShowBubbleChat();
                Debug.Log("Customer has reached the stop position");

                // Start tracking the return timer when on a stop position
                isTrackingReturnTimer = true;
            }
        }
    }

    private void MoveToReturnPosition()
    {
        // Move the customer back to the return position
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(returnXPosition, transform.position.y, transform.position.z), speed * Time.deltaTime);

        // Check if the customer has reached the return position
        if (Mathf.Approximately(transform.position.x, returnXPosition))
        {
            isReturning = false;
            Debug.Log("Customer has returned to the starting position");

            // Destroy the bubble chat if it exists
            DestroyBubbleChat();

            // Destroy the CustomerMovement game object
            Destroy(gameObject);
        }
    }

    private void UpdateReturnTimer()
    {
        // Update the return timer when on a stop position
        returnTimer += Time.deltaTime;

        // Check if the return timer exceeds the delay
        if (returnTimer >= returnDelay)
        {
            isTrackingReturnTimer = false;
            isReturning = true;

            // Decrease health or perform any other actions
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isMoving || isReturning)
        {
            // Ignore collision with FinalTaho when moving or returning
            if (other.CompareTag("FinalTaho") || other.CompareTag("FullSoya"))
            {
                Physics2D.IgnoreCollision(customerCollider, other, true);
            }
            return;
        }

        if (other.CompareTag("Player") || other.CompareTag("FullSoya"))
        {
            // Check if the customer is on the stop position
            float targetX = stopPositions[currentStopIndex];
            if (Mathf.Approximately(transform.position.x, targetX))
            {
                Debug.Log("FinalTaho collided with customer. Score increased!");

                // Add score or perform any other actions
                Destroy(other.gameObject);
                DestroyBubbleChat();

                // Destroy the FinalTaho game object
                isReturning = true;
                Destroy(other.gameObject);
            }
        }
    }

    private bool IsPositionOccupied(float targetX)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(new Vector2(targetX, transform.position.y));

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider2D collider = colliders[i];
            if (collider != customerCollider && collider.CompareTag("Customer"))
            {
                return true; // Position is occupied by another customer
            }
        }
        return false; // Position is not occupied
    }

    private void ShowBubbleChat()
    {
        if (bubbleChatPrefabs != null && bubbleChatPrefabs.Length > 0)
        {
            // Randomly choose a bubble chat prefab
            int index = Random.Range(0, bubbleChatPrefabs.Length);
            GameObject bubbleChatPrefab = bubbleChatPrefabs[index];

            // Instantiate the bubble chat prefab at the top of the customer's position
            Vector3 bubbleChatPosition = transform.position + new Vector3(0f, 2.3f, 0f); // Adjust the offset as needed
            GameObject bubbleChatObject = Instantiate(bubbleChatPrefab, bubbleChatPosition, Quaternion.identity);

            // Get the BubbleChat component and initialize it with the return delay
            BubbleChat bubbleChat = bubbleChatObject.GetComponent<BubbleChat>();
            if (bubbleChat != null)
            {
                bubbleChat.Initialize(returnDelay);
            }

            // Set the current bubble chat reference
            currentBubbleChat = bubbleChatObject;
        }
    }

    private void DestroyBubbleChat()
    {
        if (currentBubbleChat != null)
        {
            // Destroy the bubble chat instance
            Destroy(currentBubbleChat);
        }
    }

    public bool IsReturning()
    {
        return isReturning;
    }

    public static void TogglePause(bool pause)
    {
        isMovementPaused = pause;
    }
}
