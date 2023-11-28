using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LayerSprites
{
    public Sprite[] layer1;
    public Sprite[] layer2;
    public Sprite[] layer3 = new Sprite[10];
}
public class Cup : MonoBehaviour
{
    public CustomerOrder customerOrder;

    private Vector3 initialPosition;
    private Vector3 offset;

    private bool isDragging = false;
    private int selectedStage;
    private string allIngredients;

    public SpriteRenderer cupRenderer;
    private Sprite[][] firstLayerSprites;
    public Sprite[][] secondLayerSprites;
    public Sprite[][] thirdLayerSprites;

    public LayerSprites CSCS;
    public LayerSprites CLAC;
    public LayerSprites CTHM;
    public LayerSprites CCJE;
    public LayerSprites COED;
    public LayerSprites CEAT;
    public LayerSprites CBAA;

    private List<string> firstLayerIngredients = new List<string>();
    private List<string> secondLayerIngredients = new List<string>();
    private List<string> thirdLayerIngredients = new List<string>();

    private Dictionary<string, int> ingredientCombinations = new Dictionary<string, int>()
    {
        //Second Layer Combinations
        {"Pearls&Pearls", 0},
        {"Soya&Soya", 1},
        {"Syrup&Syrup", 2},
        {"Soya&Syrup", 3},
        {"Soya&Pearls", 4},
        {"Syrup&Pearls", 5},

        //third layer combinations
        {"Soya&Soya&Soya", 0},
        {"Pearls&Pearls&Pearls", 1},
        {"Syrup&Syrup&Syrup", 2},
        {"Soya&Soya&Syrup", 3},
        {"Soya&Pearls&Pearls", 4},
        {"Syrup&Pearls&Pearls", 5},
        {"Syrup&Syrup&Soya", 6},
        {"Pearls&Syrup&Soya", 7},
        {"Syrup&Syrup&Pearls", 8},
        {"Soya&Soya&Pearls", 9},
    };

    //private string ingredientsInCup = thirdLayerIngredients.ToString();
    private void Start()
    {
        initialPosition = transform.position;
        selectedStage = PlayerPrefs.GetInt("SelectedStage", 0);
        InitializeSprites();
    }
    private void InitializeSprites()
    {
        firstLayerSprites = new Sprite[][]
        {
        CSCS.layer1,
        CLAC.layer1,
        CTHM.layer1,
        CCJE.layer1,
        COED.layer1,
        CEAT.layer1,
        CBAA.layer1,
        };

        secondLayerSprites = new Sprite[][]
        {
        CSCS.layer2,
        CLAC.layer2,
        CTHM.layer2,
        CCJE.layer2,
        COED.layer2,
        CEAT.layer2,
        CBAA.layer2,
        };

        thirdLayerSprites = new Sprite[][]
        {
        CSCS.layer3,
        CLAC.layer3,
        CTHM.layer3,
        CCJE.layer3,
        COED.layer3,
        CEAT.layer3,
        CBAA.layer3,
        };
    }

    private void LogIngredients()
    {
        List<string> combinedIngredients = new List<string>();
        combinedIngredients.AddRange(firstLayerIngredients);
        combinedIngredients.AddRange(secondLayerIngredients);
        combinedIngredients.AddRange(thirdLayerIngredients);

        allIngredients = string.Join("&", combinedIngredients);
    }

    public void AddIngredient(string ingredientName)
    {
        if (firstLayerIngredients.Count < 1)
        {
            firstLayerIngredients.Add(ingredientName);
            UpdateFirstLayerSprite(ingredientName);
        }
        else if (secondLayerIngredients.Count < 1)
        {
            if (firstLayerIngredients.Count > 0)
            {
                secondLayerIngredients.Add(ingredientName);
                UpdateSecondLayerSprite(firstLayerIngredients[0], ingredientName);
            }
        }
        else if (thirdLayerIngredients.Count < 1)
        {
            if(secondLayerIngredients.Count > 0)
            {
                thirdLayerIngredients.Add(ingredientName);
                UpdateThirdLayerSprite(firstLayerIngredients[0], secondLayerIngredients[0], ingredientName);
                LogIngredients();
            }
        }
    }

    private void UpdateFirstLayerSprite(string ingredientName)
    {
        Sprite selectedSprite = GetSpriteForFirstLayer(selectedStage, ingredientName);

        if (selectedSprite != null)
        {
            cupRenderer.sprite = selectedSprite;
        }
    }
    private void UpdateSecondLayerSprite(string ingredientName1, string ingredientName2)
    {
        Sprite selectedSprite = GetSpriteForSecondLayer(selectedStage, ingredientName1, ingredientName2);

        if (selectedSprite != null)
        {
            cupRenderer.sprite = selectedSprite;
        }
    }
    private void UpdateThirdLayerSprite(string ingredientName1, string ingredientName2, string ingredientName3)
    {
        Sprite selectedSprite = GetSpriteForThirdLayer(selectedStage, ingredientName1, ingredientName2, ingredientName3);
        if (selectedSprite != null)
        {
            cupRenderer.sprite = selectedSprite;
        }
    }

    private Sprite GetSpriteForFirstLayer(int level, string ingredientName)
    {
        if (ingredientName == "Soya")
        {
            return firstLayerSprites[level][0];
        }
        else if (ingredientName == "Pearls")
        {
            return firstLayerSprites[level][1];
        }
        else if (ingredientName == "Syrup")
        {
            return firstLayerSprites[level][2];
        }

        return null;
    }
    private Sprite GetSpriteForSecondLayer(int level, string ingredientName1, string ingredientName2)
    {
        string combination1 = ingredientName1 + "&" + ingredientName2;
        string combination2 = ingredientName2 + "&" + ingredientName1;

        if (ingredientCombinations.TryGetValue(combination1, out int spriteIndex) || ingredientCombinations.TryGetValue(combination2, out spriteIndex))
        {
            if (spriteIndex >= 0 && spriteIndex < secondLayerSprites.Length)
            {
                return secondLayerSprites[level][spriteIndex];
            }
        }

        return null;
    }
    private Sprite GetSpriteForThirdLayer(int level, string ingredientName1, string ingredientName2, string ingredientName3)
    {
        string[] combinations = {
        ingredientName1 + "&" + ingredientName2 + "&" + ingredientName3,
        ingredientName1 + "&" + ingredientName3 + "&" + ingredientName2,
        ingredientName2 + "&" + ingredientName1 + "&" + ingredientName3,
        ingredientName2 + "&" + ingredientName3 + "&" + ingredientName1,
        ingredientName3 + "&" + ingredientName1 + "&" + ingredientName2,
        ingredientName3 + "&" + ingredientName2 + "&" + ingredientName1,
        ingredientName1 + "&" + ingredientName3 + "&" + ingredientName3,
        ingredientName2 + "&" + ingredientName3 + "&" + ingredientName3,
        ingredientName3 + "&" + ingredientName1 + "&" + ingredientName1,
        ingredientName3 + "&" + ingredientName2 + "&" + ingredientName2
        };

        foreach (string combination in combinations)
        {
            if (ingredientCombinations.TryGetValue(combination, out int spriteIndex))
            {
                if (spriteIndex >= 0 && spriteIndex < combinations.Length)
                {
                    return thirdLayerSprites[level][spriteIndex];
                }
            }
        }
        return null;
    } 
    private void OnMouseDown()
    {
        if (!isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = transform.position - mousePosition;
            isDragging = true;
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);

            foreach (Collider2D collider in hitColliders)
            {
                if (collider.CompareTag("Trashbin"))
                {
                    Destroy(gameObject);
                    if (customerOrder != null)
                    {
                        customerOrder.GiveToCustomer();
                    }
                    return;
                }

                if (collider.CompareTag("Customer"))
                {
                    CustomerMovement customer = collider.GetComponent<CustomerMovement>();
                    if (customer != null && customer.isMoving == false && thirdLayerIngredients.Count == 1)
                    {
                        Destroy(gameObject);
                        CustomerOrder customerOrder = customer.GetComponent<CustomerOrder>();
                        if (customerOrder != null)
                        {
                            customerOrder.GiveToCustomer();
                            customer.ReceiveOrder(customer.targetXPositions[customer.currentTargetIndex]);
                            OrderChecker orderChecker = customer.GetComponent<OrderChecker>();
                            if(orderChecker != null)
                            {
                                orderChecker.ReceiveCupIngredients(allIngredients);
                                orderChecker.CheckOrder();
                            }
                        }
                        return;
                    }
                    else
                    {
                        transform.position = initialPosition;
                    }
                }
            }
            transform.position = initialPosition;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
        }
    }
}
