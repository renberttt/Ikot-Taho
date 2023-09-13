using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashbinController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cup"))
        {
            Destroy(collision.gameObject); 
        }
    }

}
