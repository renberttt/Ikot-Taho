using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CustomerMovement : MonoBehaviour
{
    public HealthManager healthManager; 
    public CustomerOrder orderSpawner;
    private Cup cup;

    public float[] targetXPositions = { 6.75f, 2.25f, -2.25f, -6.75f };
    public float movementSpeed = 2f;
    public float queueTime = 10f;

    public int currentTargetIndex;
    public bool isMoving = true;

    private static List<float> occupiedPositions = new List<float>();

    private void Start()
    {
        SetInitialTargetPosition();
        orderSpawner.SetQueueTime(queueTime);

        if (healthManager == null)
        {
            healthManager = FindObjectOfType<HealthManager>();
        }
    }

    void Update()
    {
        if (isMoving)
        {
            float targetX = targetXPositions[currentTargetIndex];
            float direction = Mathf.Sign(targetX - transform.position.x);
            bool nextPositionOccupied = IsNextPositionOccupied(targetX);

            if (nextPositionOccupied)
            {
                SetNextTargetPosition();
            }
            else
            {
                float newPositionX = transform.position.x + direction * movementSpeed * Time.deltaTime;
                if ((direction > 0f && newPositionX >= targetX) || (direction < 0f && newPositionX <= targetX))
                {
                    transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
                    isMoving = false;
                    orderSpawner.SpawnOrderAboveCustomer(transform.position, 0.5f);
                    AddOccupiedPosition(targetX);
                    StartCoroutine(WaitAndMoveBack(targetX));
                }
                else
                {
                    transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
                }
            }
        }
    }

    private void SetNextTargetPosition()
    {
        int startingIndex = currentTargetIndex;
        int nextTargetIndex = (currentTargetIndex + 1) % targetXPositions.Length;

        while (nextTargetIndex != startingIndex && occupiedPositions.Contains(targetXPositions[nextTargetIndex]))
        {
            nextTargetIndex = (nextTargetIndex + 1) % targetXPositions.Length;
        }

        if (!occupiedPositions.Contains(targetXPositions[nextTargetIndex]))
        {
            currentTargetIndex = nextTargetIndex;
        }
        else
        {
            isMoving = false;
        }
    }
    private bool IsNextPositionOccupied(float targetX)
    {
        return occupiedPositions.Contains(targetX);
    }

    private void AddOccupiedPosition(float targetX)
    {
        occupiedPositions.Add(targetX);
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

    public void ReceiveOrder(float targetX)
    {
        occupiedPositions.Remove(targetX);
        StartCoroutine(MoveToRightAndDestroy());
    }

    private System.Collections.IEnumerator MoveToRightAndDestroy()
    {
        Vector3 originalPosition = transform.position;
        float distanceToMove = 10f;
        Vector3 targetPosition = originalPosition + Vector3.right * distanceToMove; // Move to the right

        float startTime = Time.time;
        float journeyLength = Vector3.Distance(transform.position, targetPosition);

        while (true)
        {
            float distanceCovered = (Time.time - startTime) * movementSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(originalPosition, targetPosition, fractionOfJourney);

            if (fractionOfJourney >= 1f)
            {
                Vector3 currentPosition = transform.position;
                transform.position = new Vector3(currentPosition.x, currentPosition.y, -1f); // Update the Z position to -1 (or any suitable value)
                Destroy(gameObject);
                break;
            }

            yield return null;
        }
    }

    private System.Collections.IEnumerator WaitAndMoveBack(float targetX)
    {
        yield return new WaitForSeconds(queueTime);

        Vector3 originalPosition = transform.position;
        float distanceToMove = 10f;

        Vector3 targetPosition = originalPosition + Vector3.right * distanceToMove; // Move to the right

        float startTime = Time.time;
        float journeyLength = Vector3.Distance(transform.position, targetPosition);

        while (true)
        {
            float distanceCovered = (Time.time - startTime) * movementSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(originalPosition, targetPosition, fractionOfJourney);

            if (fractionOfJourney >= 1f)
            {
                Vector3 currentPosition = transform.position;
                transform.position = new Vector3(currentPosition.x, currentPosition.y, -1f); // Update the Z position to -1 (or any suitable value)
                Destroy(gameObject);
                occupiedPositions.Remove(targetX);
                DecrementHealthBar();
                break;
            }

            yield return null;
        }
    }

    private void DecrementHealthBar()
    {
        if (healthManager != null && healthManager.healthText != null)
        {
            int currentHealth = int.Parse(healthManager.healthText.text);
            currentHealth--;
            healthManager.healthText.text = currentHealth.ToString();
        }
        else
        {
            Debug.LogWarning("No Text component found for the health bar.");
        }
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    public static void ResetOccupiedPositions()
    {
        occupiedPositions.Clear();
    }
}