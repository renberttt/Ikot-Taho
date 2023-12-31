using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerMovement : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private MainGameController mainGameController;
    private CustomerSpawner customerSpawner;
    private TimerUI timerUI;
    public CustomerOrder orderSpawner;

    public float[] targetXPositions = { 6.75f, 2.25f, -2.25f, -6.75f };
    private float movementSpeed = 5f;
    private float queueTime;
    public int currentTargetIndex;
    public bool isMoving = true;

    private static List<float> occupiedPositions = new List<float>();

    private void Start()
    {
        SetInitialTargetPosition();

        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
        }
        if (customerSpawner == null)
        {
            customerSpawner = FindObjectOfType<CustomerSpawner>();
        }
        if (mainGameController == null)
        {
            mainGameController = FindObjectOfType<MainGameController>();
        }
        timerUI = FindObjectOfType<TimerUI>();

        string difficulty = PlayerPrefs.GetString("Difficulty");
        bool playerLost = PlayerPrefs.GetInt("PlayerLost", 0) == 1;

        switch (difficulty)
        {
            case "Easy":
                queueTime = 15f;
                if (playerLost)
                {
                    queueTime += 1f;
                }
                break;
            case "Medium":
                queueTime = 10f;
                if (playerLost)
                {
                    queueTime += 1f;
                }
                break;
            case "Hard":
                queueTime = 8f;
                if (playerLost)
                {
                    queueTime += 1f;
                }
                break;
        }
        orderSpawner.SetQueueTime(queueTime);
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
                    transform.position = new Vector3(targetX, transform.position.y, 0);
                    isMoving = false;
                    orderSpawner.SpawnOrderAboveCustomer(transform.position, 0.5f);
                    AddOccupiedPosition(targetX);
                    StartCoroutine(WaitAndMoveBack(targetX));
                }
                else
                {
                    transform.position = new Vector3(newPositionX, transform.position.y, 0);
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

    private IEnumerator MoveToRightAndDestroy()
    {
        Vector3 originalPosition = transform.position;
        float distanceToMove = 10f;
        Vector3 targetPosition = originalPosition + Vector3.right * distanceToMove; 

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
                transform.position = new Vector3(currentPosition.x, currentPosition.y, -1f);
                Destroy(gameObject);
                break;
            }

            yield return null;
        }
    }

    private IEnumerator WaitAndMoveBack(float targetX)
    {
        float currentQueueTime = queueTime;
        yield return new WaitForSeconds(currentQueueTime);

        Vector3 originalPosition = transform.position;
        float distanceToMove = 10f;

        Vector3 targetPosition = originalPosition + Vector3.right * distanceToMove;

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
                transform.position = new Vector3(currentPosition.x, currentPosition.y, -1f);
                Destroy(gameObject);
                occupiedPositions.Remove(targetX);
                playerHealth.DecrementHealth();
                break;
            }

            yield return null;
        }
    }
    public static void ResetOccupiedPositions()
    {
        occupiedPositions.Clear();
    }
}