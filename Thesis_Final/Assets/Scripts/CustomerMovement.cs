using UnityEngine;
using System.Collections.Generic;

public class CustomerMovement : MonoBehaviour
{
    public float[] targetXPositions = { 6.75f, 2.25f, -2.25f, -6.75f };
    public float movementSpeed = 2f;
    public float customerSpacingDelay = 1f;
    public float queueTime = 5f;
 
    public GameObject orderPrefab;

    private int currentTargetIndex;
    private bool isMoving = true;
    private float spacingTimer;

    // Track occupied positions
    private static List<float> occupiedPositions = new List<float>();

    private Vector3 spawnPosition;

    private void Start()
    {
        SetInitialTargetPosition();
        spacingTimer = customerSpacingDelay;
        spawnPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            float targetX = targetXPositions[currentTargetIndex];
            float direction = Mathf.Sign(targetX - transform.position.x);
            bool nextPositionOccupied = IsNextPositionOccupied(direction);

            if (!nextPositionOccupied)
            {
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

                        StartCoroutine(WaitAndMoveBack());
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

    private bool IsNextPositionOccupied(float direction)
    {
        int nextTargetIndex = (currentTargetIndex + 1) % targetXPositions.Length;
        float nextTargetX = targetXPositions[nextTargetIndex];

        return occupiedPositions.Contains(nextTargetX);
    }

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

    private System.Collections.IEnumerator WaitAndMoveBack()
    {
        yield return new WaitForSeconds(queueTime);

        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = spawnPosition;

        float journeyLength = Vector3.Distance(transform.position, targetPosition);
        float startTime = Time.time;

        while (true)
        {
            float distanceCovered = (Time.time - startTime) * movementSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(originalPosition, targetPosition, fractionOfJourney);

            if (fractionOfJourney >= 1f)
            {
                Destroy(gameObject);
                break;
            }

            yield return null;
        }
    }
    public void StopMoving()
    {
        isMoving = false;
    }
}
