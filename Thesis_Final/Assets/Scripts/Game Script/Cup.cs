using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 offset;

    private bool isDragging = false;

    public SpriteRenderer cupRenderer;
    public Sprite[] firstLayerSprites;
    public Sprite[] secondLayerSprites;
    public Sprite[] thirdLayerSprites;

    private List<string> firstLayerIngredients = new List<string>();
    private List<string> secondLayerIngredients = new List<string>();
    private List<string> thirdLayerIngredients = new List<string>();

    private void Start()
    {
        initialPosition = transform.position;
    }

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

    private void LogIngredients()
    {
        string firstLayerIngredientsStr = "First Layer Ingredients: " + string.Join(", ", firstLayerIngredients.ToArray());
        string secondLayerIngredientsStr = "Second Layer Ingredients: " + string.Join(", ", secondLayerIngredients.ToArray());
        string thirdLayerIngredientsStr = "Third Layer Ingredients: " + string.Join(", ", thirdLayerIngredients.ToArray());

        Debug.Log(firstLayerIngredientsStr);
        Debug.Log(secondLayerIngredientsStr);
        Debug.Log(thirdLayerIngredientsStr);
    }

    public void AddIngredient(string ingredientName)
    {
        if (firstLayerIngredients.Count < 1)
        {
            firstLayerIngredients.Add(ingredientName);
            UpdateFirstLayerSprite(ingredientName);
            LogIngredients();
        }
        else if (secondLayerIngredients.Count < 1)
        {
            if (firstLayerIngredients.Count > 0)
            {
                secondLayerIngredients.Add(ingredientName);
                UpdateSecondLayerSprite(firstLayerIngredients[0], ingredientName);
                LogIngredients();
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
        Sprite selectedSprite = GetSpriteForFirstLayer(ingredientName);

        if (selectedSprite != null)
        {
            cupRenderer.sprite = selectedSprite;
        }
    }
    private void UpdateSecondLayerSprite(string ingredientName1, string ingredientName2)
    {
        Sprite selectedSprite = GetSpriteForSecondLayer(ingredientName1, ingredientName2);

        if (selectedSprite != null)
        {
            cupRenderer.sprite = selectedSprite;
        }
    }
    private void UpdateThirdLayerSprite(string ingredientName1, string ingredientName2, string ingredientName3)
    {
        Sprite selectedSprite = GetSpriteForThirdLayer(ingredientName1, ingredientName2, ingredientName3);

        if (selectedSprite != null)
        {
            cupRenderer.sprite = selectedSprite;
        }
    }

    private Sprite GetSpriteForFirstLayer(string ingredientName)
    {
        if (ingredientName == "Soya")
        {
            return firstLayerSprites[0];
        }
        else if (ingredientName == "Pearls")
        {
            return firstLayerSprites[1];
        }
        else if (ingredientName == "Syrup")
        {
            return firstLayerSprites[2];
        }

        return null;
    }
    private Sprite GetSpriteForSecondLayer(string ingredientName1, string ingredientName2)
    {
        string combination1 = ingredientName1 + "&" + ingredientName2;
        string combination2 = ingredientName2 + "&" + ingredientName1;

        if (ingredientCombinations.TryGetValue(combination1, out int spriteIndex) || ingredientCombinations.TryGetValue(combination2, out spriteIndex))
        {
            if (spriteIndex >= 0 && spriteIndex < secondLayerSprites.Length)
            {
                return secondLayerSprites[spriteIndex];
            }
        }

        return null;
    }
    private Sprite GetSpriteForThirdLayer(string ingredientName1, string ingredientName2, string ingredientName3)
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
                if (spriteIndex >= 0 && spriteIndex < thirdLayerSprites.Length)
                {
                    return thirdLayerSprites[spriteIndex];
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
                    return;
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
