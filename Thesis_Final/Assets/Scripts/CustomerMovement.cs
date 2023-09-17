using System.Collections;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    public float speed = 2.0f; // Movement speed of the customer

    private float minX;
    private float maxX;
    private float stopTime = 5.0f; // Time to stop at a position
    private bool isMovingRight = true; // Flag to track movement direction
    private static bool isMovementPaused;
    private void Start()
    {
        // Calculate the minX and maxX based on the screen width
        float screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        minX = -screenHalfWidth;
        maxX = screenHalfWidth;

        // Start moving the customer initially
        MoveInDirection(isMovingRight);
    }

    private void Update()
    {
        // Check if the customer has moved out of the screen
        if ((isMovingRight && transform.position.x > maxX) || (!isMovingRight && transform.position.x < minX))
        {
            // Change direction when reaching the edge of the screen
            isMovingRight = !isMovingRight;

            // Start moving in the new direction
            MoveInDirection(isMovingRight);
        }
    }

    private void MoveInDirection(bool moveRight)
    {
        // Calculate the target position based on the direction
        float targetX = moveRight ? maxX : minX;

        // Move the customer to the target position
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        float distance = Vector3.Distance(transform.position, targetPosition);
        float duration = distance / speed;

        StartCoroutine(MoveToTarget(targetPosition, duration));
    }

    private IEnumerator MoveToTarget(Vector3 targetPosition, float duration)
    {
        float startTime = Time.time;
        Vector3 startPosition = transform.position;

        while (Time.time - startTime < duration)
        {
            float journeyFraction = (Time.time - startTime) / duration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, journeyFraction);
            yield return null;
        }

        // Set the final position to ensure accuracy
        transform.position = targetPosition;

        // Wait for the specified stop time
        yield return new WaitForSeconds(stopTime);

        // Destroy the customer after stopping
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the customer collided with another customer
        if (other.CompareTag("Customer"))
        {
            // Change direction when colliding with another customer
            isMovingRight = !isMovingRight;

            // Start moving in the new direction
            MoveInDirection(isMovingRight);
        }
    }
    public static void TogglePause(bool pause)
    {
        isMovementPaused = pause;
    }
}
