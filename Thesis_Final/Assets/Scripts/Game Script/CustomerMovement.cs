using UnityEngine;
using System.Collections.Generic;

public class CustomerMovement : MonoBehaviour
{
    public float[] targetXPositions = { 6.75f, 2.25f, -2.25f, -6.75f };
    public float movementSpeed = 2f;
    public float queueTime = 5f;
    public CustomerOrder orderSpawner;
    public Cup cup;
    private int currentTargetIndex;
    private bool isMoving = true;

    // Track occupied positions
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
            bool nextPositionOccupied = IsNextPositionOccupied();

            if (!nextPositionOccupied)
            {
                float newPositionX = transform.position.x + direction * movementSpeed * Time.deltaTime;
                if ((direction > 0f && newPositionX >= targetX) || (direction < 0f && newPositionX <= targetX))
                {
                    transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
                    isMoving = false;
                    orderSpawner.SpawnOrderAboveCustomer(transform.position, 0.5f);
                    SetNextTargetPosition();
                    StartCoroutine(WaitAndMoveBack(targetX));
                    occupiedPositions.Add(targetX);

                }
                else
                {
                    transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
                }
            }
        }
    }

    private bool IsNextPositionOccupied()
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

    public System.Collections.IEnumerator WaitAndMoveBack(float targetX)
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
                break;
            }

            yield return null;
        }
    }

    public void ServeCustomer()
    {
        if (GetComponent<Collider2D>().IsTouching(cup.GetComponent<Collider2D>()))
        {
            orderSpawner.GiveToCustomer();
        }
    }
    public void StopMoving()
    {
        isMoving = false;
    }
}
