using UnityEngine;
using System.Collections.Generic;

public class CustomerMovement : MonoBehaviour
{
    public float[] targetXPositions = { 6.75f, 2.25f, -2.25f, -6.75f };
    public float movementSpeed = 2f;
    public float customerSpacingDelay = 1f; // Delay between customers

    private int currentTargetIndex;
    private bool isMoving = true;
    private float spacingTimer; // Timer to control customer spacing

    // Track occupied positions
    private static List<float> occupiedPositions = new List<float>();

    private void Start()
    {
        SetInitialTargetPosition();
        spacingTimer = customerSpacingDelay;
    }

    void Update()
    {
        if (isMoving)
        {
            float targetX = targetXPositions[currentTargetIndex];
            float direction = Mathf.Sign(targetX - transform.position.x);

            // Check if the next position is occupied by another customer
            bool nextPositionOccupied = IsNextPositionOccupied(direction);

            if (!nextPositionOccupied)
            {
                // Check the spacing timer
                if (spacingTimer <= 0f)
                {
                    float newPositionX = transform.position.x + direction * movementSpeed * Time.deltaTime;
                    if ((direction > 0f && newPositionX >= targetX) || (direction < 0f && newPositionX <= targetX))
                    {
                        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
                        isMoving = false;
                        occupiedPositions.Add(targetX);
                        spacingTimer = customerSpacingDelay;
                        SetNextTargetPosition();
                    }
                    else
                    {
                        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
                    }
                }
                else
                {
                    spacingTimer -= Time.deltaTime;
                }
            }
        }
    }

    // Check if the next position is occupied by another customer
    private bool IsNextPositionOccupied(float direction)
    {
        int nextTargetIndex = (currentTargetIndex + 1) % targetXPositions.Length;
        float nextTargetX = targetXPositions[nextTargetIndex];

        // Check if the next position is in the list of occupied positions
        return occupiedPositions.Contains(nextTargetX);
    }

    // Set the initial target position to an available slot
    private void SetInitialTargetPosition()
    {
        for (int i = 0; i < targetXPositions.Length; i++)
        {
            if (!occupiedPositions.Contains(targetXPositions[i]))
            {
                currentTargetIndex = i;
                return;
            }
        }
    }

    // Set the next available target position
    private void SetNextTargetPosition()
    {
        int nextTargetIndex = (currentTargetIndex + 1) % targetXPositions.Length;
        float nextTargetX = targetXPositions[nextTargetIndex];

        while (occupiedPositions.Contains(nextTargetX))
        {
            nextTargetIndex = (nextTargetIndex + 1) % targetXPositions.Length;
            nextTargetX = targetXPositions[nextTargetIndex];
        }

        currentTargetIndex = nextTargetIndex;
    }

    public void StopMoving()
    {
        isMoving = false;
    }
}
