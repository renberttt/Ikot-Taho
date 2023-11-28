using System;
using UnityEngine;

public class OrderChecker : MonoBehaviour
{
    public Cup cup;
    private ScoreText scoreText;
    private PlayerHealth playerHealth;
    private GoodsLang goodsLang;
    private Payaman payaman;
    private string[] cupIngredients;
    private string[] customerOrder;
    private void Start()
    {
        if (cup == null)
        {
            cup = FindObjectOfType<Cup>();
        }
        if (scoreText == null)
        {
            scoreText = FindObjectOfType<ScoreText>();
        }
        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
        }
        if (goodsLang == null)
        {
            goodsLang = FindObjectOfType<GoodsLang>();
        }
        if (payaman == null)
        {
            payaman = FindObjectOfType<Payaman>();
        }

    }

    public void CheckOrder()
    {
        string difficulty = PlayerPrefs.GetString("Difficulty");
        if (cupIngredients != null && customerOrder != null)
        {
            Array.Sort(cupIngredients);
            Array.Sort(customerOrder);

            if (ArraysEqual(customerOrder, cupIngredients) || goodsLang.isGoodsOn == true)
            {
                switch (difficulty)
                {
                    case "Easy":
                        if (payaman.isPayamanOn == true)
                        {
                            scoreText.IncrementScore(100);
                        }
                        else
                        {
                            scoreText.IncrementScore(50);
                        }
                        break;
                    case "Medium":
                        if (payaman.isPayamanOn == true)
                        {
                            scoreText.IncrementScore(90);
                        }
                        else
                        {
                            scoreText.IncrementScore(45);
                        }
                        break;
                    case "Hard":
                        if (payaman.isPayamanOn == true)
                        {
                            scoreText.IncrementScore(80);
                        }
                        else
                        {
                            scoreText.IncrementScore(40);
                        }
                        break;
                }
            }
            else
            {
                switch (difficulty)
                {
                    case "Easy":
                        scoreText.IncrementScore(10);
                        break;
                    case "Medium":
                        scoreText.IncrementScore(5);
                        break;
                    case "Hard":
                        playerHealth.DecrementHealth();
                        break;
                }
            }
        }
        else
        {
            Debug.LogError("Arrays are null. Unable to check order.");
        }
    }

    private bool ArraysEqual(string[] arr1, string[] arr2)
    {
        if (arr1.Length != arr2.Length)
            return false;

        for (int i = 0; i < arr1.Length; i++)
        {
            if (arr1[i] != arr2[i])
                return false;
        }

        return true;
    }

    public void ReceiveCustomerOrder(string customerOrder)
    {
        this.customerOrder = customerOrder.Split('&');
    }

    public void ReceiveCupIngredients(string cupIngredients)
    {
        this.cupIngredients = cupIngredients.Split('&');
    }
}
