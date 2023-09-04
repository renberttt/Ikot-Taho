using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashbinController : MonoBehaviour
{
    private bool CheckCollision()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider != GetComponent<Collider2D>() && collider.CompareTag("Cup"))
            {
                return true; // Collision detected with a Trashbin object
            }
        }
        return false;
    }


}
