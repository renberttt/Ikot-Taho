using TMPro;
using UnityEngine;

public class ShopCoin : MonoBehaviour
{
    private int coinValue; 
    private TextMeshProUGUI textComponent;

    void Start()
    { 
        coinValue = PlayerPrefs.GetInt("PlayerMoney",999999);
        textComponent = GetComponent<TextMeshProUGUI>();

        if (textComponent != null)
        {
            UpdateCoinText();
        }
    }
    public void UpdateCoinText()
    {
        textComponent.text = coinValue.ToString();
        PlayerPrefs.SetInt("PlayerMoney", coinValue);
    }
    public void DecreaseCoins(int value)
    {
        coinValue -= value;
    }
    public bool HasEnoughCoins(int value)
    {
        return coinValue >= value;
    }
}
