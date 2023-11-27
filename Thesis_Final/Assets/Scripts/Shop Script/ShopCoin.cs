using TMPro;
using UnityEngine;

public class ShopCoin : MonoBehaviour
{
    public int coinValue; 

    private TextMeshProUGUI textComponent;

    void Start()
    {
       
        int playerMoney = PlayerPrefs.GetInt("PlayerMoney", 5000);

       
        coinValue = playerMoney;

       
        textComponent = GetComponent<TextMeshProUGUI>();

        if (textComponent != null)
        {
        
            UpdateCoinText();
        }
    }

    void Update()
    {
        // Additional logic can be added here if needed
    }

    public void UpdateCoinText()
    {
        
        textComponent.text = coinValue.ToString();
    }
}
