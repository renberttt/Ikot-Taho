using UnityEngine;

public class CustomerOrder : MonoBehaviour
{
    public CustomerMovement customerMovement;
    public CustomerSpawner customerSpawner;
    public GameObject orderPrefab;

    public Sprite[] cscsCustomer;
    public Sprite[] clacCustomer;
    public Sprite[] cthmCustomer;
    public Sprite[] ccjeCustomer;
    public Sprite[] coedCustomer;
    public Sprite[] ceatCustomer;
    public Sprite[] cbaaCustomer;
    
    private Sprite[] selectedCustomerSet;
    private GameObject spawnedOrder;
    private float queueTime;
    private void Start()
    {
        SetSelectedCustomerSet();
    }
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

        SpriteRenderer spriteRenderer = spawnedOrder.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && selectedCustomerSet.Length > 0)
        {
            int randomIndex = Random.Range(0, selectedCustomerSet.Length);
            spriteRenderer.sprite = selectedCustomerSet[randomIndex];
        }
    }

    private System.Collections.IEnumerator RemoveOrderAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (spawnedOrder != null)
        {
            Destroy(spawnedOrder);
        }
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
}