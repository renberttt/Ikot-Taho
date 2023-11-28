using UnityEngine;
using System.Collections;
public class ShopScript : MonoBehaviour
{
    private ShopCoin shopCoin;
    public GameObject warningText;
    public GameObject boughtText;
    private int coinValue = 500;

    public GameObject[] shopItems;
    private SpriteRenderer[] renderersToEnable;
    private BoxCollider2D[] collidersToEnable;

    private int currentIndex = 0;

    private void Start()
    {
        shopCoin = FindObjectOfType<ShopCoin>();
        renderersToEnable = new SpriteRenderer[shopItems.Length];
        collidersToEnable = new BoxCollider2D[shopItems.Length];

        for (int i = 0; i < shopItems.Length; i++)
        {
            renderersToEnable[i] = shopItems[i].GetComponent<SpriteRenderer>();
            collidersToEnable[i] = shopItems[i].GetComponent<BoxCollider2D>();

            bool purchased = PlayerPrefs.GetInt("ShopItem_" + i, 0) == 1;

            if (purchased || i == 0)
            {
                EnableObjectAtIndex(i);
            }
            else
            {
                renderersToEnable[i].color = Color.black;
                collidersToEnable[i].enabled = false;
            }
        }
    }

    void Update()
{
    if (Input.GetMouseButtonDown(0))
    {
        Vector2 rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

        if (hit.collider != null)
        {
            GameObject clickedObject = hit.collider.gameObject;

            if (clickedObject.CompareTag("Clickable"))
            {
                if (shopCoin != null && shopCoin.HasEnoughCoins(coinValue))
                {
                    shopCoin.DecreaseCoins(coinValue);
                    shopCoin.UpdateCoinText();
                    
                    PlayerPrefs.SetInt("ShopItem_" + currentIndex, 1);
                    PlayerPrefs.Save();

                    bool purchased = PlayerPrefs.GetInt("ShopItem_" + currentIndex, 0) == 1;
                    if (purchased && currentIndex != 0) // Exclude the first item
                    {
                        boughtText.SetActive(true);
                        StartCoroutine(HideBoughtText());
                    }
                    
                    if (currentIndex < shopItems.Length)
                    {
                        DisableObjectAtIndex(currentIndex);
                        currentIndex++;
                        if (currentIndex < shopItems.Length)
                        {
                            EnableObjectAtIndex(currentIndex);
                        }
                    }
                }
                else
                {
                    warningText.SetActive(true);
                    StartCoroutine(Show());
                }
            }
        }
    }
}

    void DisableObjectAtIndex(int index)
    {
        if (index >= 0 && index < shopItems.Length)
        {
            collidersToEnable[index].enabled = false;
        }
    }

    void EnableObjectAtIndex(int index)
    {
        if (index >= 0 && index < shopItems.Length)
        {
            renderersToEnable[index].color = Color.white;
            collidersToEnable[index].enabled = true;
        }
    }
    private IEnumerator HideBoughtText()
    {
        yield return new WaitForSeconds(1);
        boughtText.SetActive(false);
    }
    private IEnumerator Show()
    {
        yield return new WaitForSeconds(1);
        warningText.SetActive(false);
    }
}