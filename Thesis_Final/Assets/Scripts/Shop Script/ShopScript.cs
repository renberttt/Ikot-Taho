using UnityEngine;
using TMPro;
using System.Collections;

public class ShopScript : MonoBehaviour
{
    private ShopCoin shopCoin;
    public GameObject warningText;
    public GameObject boughtText;
    public TMP_Text boughtTMP;
    private int coinValue = 500;

    public GameObject[] shopItems;
    public Sprite[] boughtSprite;
    private SpriteRenderer[] renderersToEnable;
    private BoxCollider2D[] collidersToEnable;
    private bool[] purchasedItems;

    private int currentIndex = 0;

    private void Start()
    {
        shopCoin = FindObjectOfType<ShopCoin>();
        renderersToEnable = new SpriteRenderer[shopItems.Length];
        collidersToEnable = new BoxCollider2D[shopItems.Length];
        purchasedItems = new bool[shopItems.Length];

        for (int i = 0; i < shopItems.Length; i++)
        {
            renderersToEnable[i] = shopItems[i].GetComponent<SpriteRenderer>();
            collidersToEnable[i] = shopItems[i].GetComponent<BoxCollider2D>();

            bool purchased = PlayerPrefs.GetInt("ShopItem_" + i, 0) == 1;
            purchasedItems[i] = purchased;

            if (purchased)
            {
                EnableObjectAtIndex(i);
                collidersToEnable[i].enabled = false;
            }
            else
            {
                renderersToEnable[i].color = Color.black;
                collidersToEnable[i].enabled = true;
            }
        }

        for (int i = 0; i < purchasedItems.Length; i++)
        {
            if (!purchasedItems[i])
            {
                currentIndex = i;
                renderersToEnable[currentIndex].color = Color.white;
                break;
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
                string objectName = clickedObject.name;
                if (clickedObject.CompareTag("Clickable"))
                {
                    if (!purchasedItems[currentIndex])
                    {
                        if (shopCoin != null && shopCoin.HasEnoughCoins(coinValue))
                        {
                            shopCoin.DecreaseCoins(coinValue);
                            shopCoin.UpdateCoinText();

                            purchasedItems[currentIndex] = true;
                            renderersToEnable[currentIndex].color = Color.white;
                            renderersToEnable[currentIndex].sprite = boughtSprite[currentIndex];
                            collidersToEnable[currentIndex].enabled = false;

                            boughtText.SetActive(true);
                            StartCoroutine(BoughtItem(objectName));
                            PlayerPrefs.SetInt("ShopItem_" + currentIndex, 1);

                            for (int i = currentIndex + 1; i < purchasedItems.Length; i++)
                            {
                                if (!purchasedItems[i])
                                {
                                    currentIndex = i;
                                    renderersToEnable[currentIndex].color = Color.white;
                                    break;
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
    }

    void EnableObjectAtIndex(int index)
    {
        if (index >= 0 && index < shopItems.Length)
        {
            renderersToEnable[index].sprite = boughtSprite[index];
            renderersToEnable[index].color = Color.white;

        }
    }
    private IEnumerator BoughtItem(string name)
    {
        boughtTMP.text = name + " Bought!";
        yield return new WaitForSeconds(3);
        boughtText.SetActive(false);
    }
    private IEnumerator Show()
    {
        yield return new WaitForSeconds(1);
        warningText.SetActive(false);
    }
}
