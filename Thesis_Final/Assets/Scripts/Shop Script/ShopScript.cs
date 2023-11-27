using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public ShopCoin shopCoin;
    public int coinValue = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                if (shopCoin != null)
                {
                    shopCoin.coinValue -= coinValue;
                    shopCoin.UpdateCoinText();
                    Debug.Log("Clicked on " + gameObject.name);

                    // Check the name or tag of the clicked game object and update GameManager accordingly
                    if (gameObject.CompareTag("Pandan"))
                    {
                        GameManager.Instance.shopClickedPandan = true;
                    }
                    if (gameObject.CompareTag("Chocolate"))
                    {
                        GameManager.Instance.shopClickedChocolate = true;
                    }
                     if (gameObject.CompareTag("Mango"))
                    {
                        GameManager.Instance.shopClickedMango = true;
                    }
                     if (gameObject.CompareTag("Ube"))
                    {
                        GameManager.Instance.shopClickedUbe = true;
                    }
                     if (gameObject.CompareTag("Tapioca"))
                    {
                        GameManager.Instance.shopClickedTapioca = true;
                    }
                     if (gameObject.CompareTag("Strawberry"))
                    {
                        GameManager.Instance.shopClickedStrawberry = true;
                    }
                }
                else
                {
                    Debug.LogError("ShopCoin reference is null!");
                }
            }
        }
    }
}
