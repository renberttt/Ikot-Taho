using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public ShopCoin shopCoin; // Reference to the ShopCoin script
    public int coinValue = 0; // Coin value for this clickable object

    void Start()
    {
        // You can initialize any object-specific settings here
    }

    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits the GameObject with this script
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                // If there is a collision with this GameObject, decrease the coin value
                shopCoin.coinValue -= coinValue;
                shopCoin.UpdateCoinText();
                Debug.Log("Clicked on " + gameObject.name);
            }
        }
    }
}
