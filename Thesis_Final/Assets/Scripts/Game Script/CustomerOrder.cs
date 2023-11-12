using UnityEngine;

public class CustomerOrder : MonoBehaviour
{
    public CustomerMovement customerMovement;
    public GameObject orderPrefab;
    public GameObject spawnedOrder;
    private float queueTime;

    public void SetQueueTime(float time)
    {
        queueTime = time;
    }
    public void SpawnOrderAboveCustomer(Vector3 customerPosition, float spawnDelay)
    {
        StartCoroutine(SpawnOrderWithDelay(customerPosition, spawnDelay));
        StartCoroutine(RemoveOrderAfterTime(queueTime));
    }
    public void GiveToCustomer()
    {
        if (spawnedOrder != null)
        {
            Destroy(spawnedOrder);
        }
    }
    private System.Collections.IEnumerator SpawnOrderWithDelay(Vector3 customerPosition, float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);

        Vector3 orderPosition = new Vector3(customerPosition.x + 1f, 3f, customerPosition.z);
        spawnedOrder = Instantiate(orderPrefab, orderPosition, Quaternion.identity);
    }
    private System.Collections.IEnumerator RemoveOrderAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (spawnedOrder != null)
        {
            Destroy(spawnedOrder);
        }
    }
}
