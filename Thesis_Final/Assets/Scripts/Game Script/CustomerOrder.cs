using UnityEngine;
using System.Collections;
[System.Serializable]
public class CustomerOrderInfo
{
    public Sprite sprite;
    public string ingredients;

    public CustomerOrderInfo(Sprite sprite, string orderIngredients)
    {
        this.sprite = sprite;
        this.ingredients = orderIngredients;
    }
}


public class CustomerOrder : MonoBehaviour
{
    private OrderChecker orderChecker;
    public GameObject orderPrefab;

    public CustomerOrderInfo[] cscsCustomer;
    public CustomerOrderInfo[] clacCustomer;
    public CustomerOrderInfo[] cthmCustomer;
    public CustomerOrderInfo[] ccjeCustomer;
    public CustomerOrderInfo[] coedCustomer;
    public CustomerOrderInfo[] ceatCustomer;
    public CustomerOrderInfo[] cbaaCustomer;

    private CustomerOrderInfo[] selectedCustomerSet;
    private GameObject spawnedOrder;
    private float queueTime;
    private void Start()
    {
        SetSelectedCustomerSet();
        if (orderChecker == null)
        {
            orderChecker = FindObjectOfType<OrderChecker>();
        }
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
    private IEnumerator SpawnOrderWithDelay(Vector3 customerPosition, float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);

        Vector3 orderPosition = new Vector3(customerPosition.x + 1f, 3f, customerPosition.z);
        spawnedOrder = Instantiate(orderPrefab, orderPosition, Quaternion.identity);

        SpriteRenderer spriteRenderer = spawnedOrder.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && selectedCustomerSet.Length > 0)
        {
            int randomIndex = Random.Range(0, selectedCustomerSet.Length);
            spriteRenderer.sprite = selectedCustomerSet[randomIndex].sprite;
            string customerOrder = selectedCustomerSet[randomIndex].ingredients;
            orderChecker.ReceiveCustomerOrder(customerOrder);
        }
    }

    private IEnumerator RemoveOrderAfterTime(float delay)
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