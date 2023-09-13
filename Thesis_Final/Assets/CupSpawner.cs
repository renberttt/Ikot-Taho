using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupSpawner : MonoBehaviour
{
    public GameObject emptyCupPrefab; // Reference to the empty cup prefab in the Inspector.
    private GameObject currentCup;
    private AudioSource audioSource;
    private Vector3 fixedSpawnPosition = new Vector3(0, -3.2f, 0f);

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            Collider2D[] hitColliders = Physics2D.OverlapPointAll(mousePosition);
            foreach (Collider2D collider in hitColliders)
            {
                if (collider.CompareTag("StackedCups"))
                {
                    // Instantiate the empty cup prefab at the mouse position.
                    if (currentCup != null)
                    {
                        Destroy(currentCup);
                    }
                    if (audioSource != null)
                    {
                        audioSource.Play();
                    }
                    currentCup = Instantiate(emptyCupPrefab, fixedSpawnPosition, Quaternion.identity);
                    break; // Exit the loop after creating one cup.
                }
            }
        }
    }
}
