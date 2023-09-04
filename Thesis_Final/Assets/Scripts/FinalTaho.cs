using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTaho : MonoBehaviour
{
    private bool isDragging;
    private Vector3 offset;

    void Start()
    {
        isDragging = false;
        offset = Vector3.zero;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 newPosition = offset + Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = newPosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (CheckCollision())
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private bool CheckCollision()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider != GetComponent<Collider2D>() && collider.CompareTag("Trashbin"))
            {
                return true; // Collision detected with a Trashbin object
            }
        }
        return false;
    }
}
