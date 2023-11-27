using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public SpriteRenderer nextRenderers;
    public BoxCollider2D nextColliders;

    private ShopCoin shopCoin;
    public int coinValue = 0;
    public bool isBought;
    private void Start()
    {
        shopCoin = FindObjectOfType<ShopCoin>();

        nextRenderers.color = Color.black;
        nextColliders.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (shopCoin != null)
                {
                    shopCoin.DecreaseCoins(coinValue);
                    shopCoin.UpdateCoinText();
                    isBought = true;
                    EnableNextObject();
                }
            }
        }
    }

    void EnableNextObject()
    {
         nextRenderers.color = Color.white;
         nextColliders.enabled = true;
    }
}
