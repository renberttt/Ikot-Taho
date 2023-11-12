using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CustomerMovement : MonoBehaviour
{
    public CustomerOrder orderSpawner;
    private Cup cup;
    public Text healthText;

    public float[] targetXPositions = { 6.75f, 2.25f, -2.25f, -6.75f };
    public float movementSpeed = 2f;
    public float queueTime = 5f;


    private int currentTargetIndex;
    public bool isMoving = true;

    private static List<float> occupiedPositions = new List<float>();

    private Vector3 spawnPosition;

    private void Start()
    {
        SetInitialTargetPosition();
        spawnPosition = transform.position;
        orderSpawner.SetQueueTime(queueTime);
    }

    void Update()
    {
        if (isMoving)
        {
            float targetX = targetXPositions[currentTargetIndex];
            float direction = Mathf.Sign(targetX - transform.position.x);
            bool nextPositionOccupied = IsNextPositionOccupied(targetX);

            if (!nextPositionOccupied)
            {
                float newPositionX = transform.position.x + direction * movementSpeed * Time.deltaTime;
                if ((direction > 0f && newPositionX >= targetX) || (direction < 0f && newPositionX <= targetX))
                {
                    transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
                    isMoving = false;
                    orderSpawner.SpawnOrderAboveCustomer(transform.position, 0.5f);
                    AddOccupiedPosition(targetX);
                    Debug.Log("Occupied Positions: " + string.Join(", ", occupiedPositions));
                }
                else
                {
                    transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
                }
            }
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

    public void ReceiveOrder(float currentXPosition)
    {
        Debug.Log("RECEIVED");
        Destroy(gameObject);
        occupiedPositions.Remove(currentXPosition);
    }

    private System.Collections.IEnumerator WaitAndMoveBack(float targetX)
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
                occupiedPositions.Remove(targetX);
                Debug.LogWarning("Occupied Positions: " + string.Join(", ", occupiedPositions));
                DecrementHealthBar();
                break;
            }

            yield return null;
        }
    }

    private void DecrementHealthBar()
    {
        if (healthText != null)
        {
            int currentHealth = int.Parse(healthText.text);

            // bawas buhay
            currentHealth--;

            Debug.Log("-1 HP");


            healthText.text = currentHealth.ToString();
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
}