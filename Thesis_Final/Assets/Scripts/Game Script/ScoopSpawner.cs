using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoopSpawner : MonoBehaviour
{
    public GameObject scoopPrefab;

    public Sprite[] soyaSprite;
    public Sprite[] pearlSprite;
    public Sprite[] syrupSprite;
    
    private GameObject currentScoop;
    private bool canInteractWithScoop = true;
    private bool isDragging = false;
    private Vector3 offset;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing.");
        }
    }

    private void OnMouseDown()
    {
        if (canInteractWithScoop)
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0f;

            currentScoop = Instantiate(scoopPrefab, clickPosition, Quaternion.identity);
            ChangeScoopSpriteBasedOnIndex();
            canInteractWithScoop = false;
            offset = currentScoop.transform.position - clickPosition;
            isDragging = true;

            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            Vector2 boxSize = currentScoop.GetComponent<PolygonCollider2D>().bounds.size;

            Collider2D[] colliders = Physics2D.OverlapBoxAll(currentScoop.transform.position, boxSize, 0f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Cup"))
                {
                    Destroy(currentScoop);
                    Cup cup = collider.GetComponent<Cup>();
                    if (cup != null)
                    {
                        string ingredientTag = currentScoop.tag;
                        cup.AddIngredient(ingredientTag);
                    }
                    break;
                }
            }
            Destroy(currentScoop);
            canInteractWithScoop = true;
            isDragging = false;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentScoop.transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, currentScoop.transform.position.z);
        }
    }
    private void ChangeScoopSpriteBasedOnIndex()
    {
        int selectedImageIndex = PlayerPrefs.GetInt("SelectedStage", 0);

        SpriteRenderer spriteRenderer = currentScoop.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            string scoopName = currentScoop.name;
            if (scoopName == "Taho Scoop(Clone)")
            {
                spriteRenderer.sprite = soyaSprite[selectedImageIndex];
            }
            else if (scoopName == "Pearl Scoop(Clone)")
            {
                spriteRenderer.sprite = pearlSprite[selectedImageIndex];
            }
            else if (scoopName == "Syrup Scoop(Clone)")
            {
                spriteRenderer.sprite = syrupSprite[selectedImageIndex];
            }
            else
            {
                Debug.LogWarning("Selected scoop not recognized.");
            }
        }
        else
        {
            Debug.LogWarning("SpriteRenderer not found.");
        }
    }

}
