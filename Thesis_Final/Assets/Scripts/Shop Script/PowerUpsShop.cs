using UnityEngine;
using System.Collections;

public class PowerUpsShop : MonoBehaviour
{
    private ShopCoin shopCoin;
    public GameObject warningText;
    private int coinValue = 1000;
    public bool isBought;

    public GameObject[] shopItems;
    private BoxCollider2D[] collidersToEnable;
    private bool[] canClick;

    private void Start()
    {
        shopCoin = FindObjectOfType<ShopCoin>();
        collidersToEnable = new BoxCollider2D[shopItems.Length];
        canClick = new bool[shopItems.Length];

        for (int i = 0; i < shopItems.Length; i++)
        {
            collidersToEnable[i] = shopItems[i].GetComponent<BoxCollider2D>();

            bool purchased = PlayerPrefs.GetInt("PowerUp_" + i, 0) == 1;

            if (purchased)
            {
                EnableObjectAtIndex(i);
                canClick[i] = false;
            }
            else
            {
                shopItems[i].GetComponent<SpriteRenderer>().color = Color.grey;
                canClick[i] = true;
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
                int clickedIndex = GetIndexFromGameObject(clickedObject);

                if (clickedIndex != -1 && canClick[clickedIndex])
                {
                    if (shopCoin != null && shopCoin.HasEnoughCoins(coinValue))
                    {
                        shopCoin.DecreaseCoins(coinValue);
                        shopCoin.UpdateCoinText();
                        canClick[clickedIndex] = false;
                        EnableObjectAtIndex(clickedIndex);

                        PlayerPrefs.SetInt("PowerUp_" + clickedIndex, 1);
                        PlayerPrefs.Save();
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
    void EnableObjectAtIndex(int index)
    {
        if (index >= 0 && index < shopItems.Length)
        {
            shopItems[index].GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    void DisableObjectAtIndex(int index)
    {
        if (index >= 0 && index < shopItems.Length)
        {
            collidersToEnable[index].enabled = false;
        }
    }

    int GetIndexFromGameObject(GameObject obj)
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (obj == shopItems[i])
            {
                return i;
            }
        }
        return -1;
    }

    private IEnumerator Show()
    {
        yield return new WaitForSeconds(1);
        warningText.SetActive(false);
    }
}