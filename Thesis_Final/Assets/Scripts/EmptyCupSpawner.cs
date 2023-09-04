using UnityEngine;
using UnityEngine.UI;

public class EmptyCupSpawner : MonoBehaviour
{
    public GameObject emptyCupPrefab; // The prefab to spawn

    // Attach this method to the button's OnClick event in the Inspector.
    public void SpawnEmptyCupOnClick()
    {
        // Check if there are existing game objects tagged as specified
        if (GameObject.FindGameObjectWithTag("FinalTaho") != null || GameObject.FindGameObjectWithTag("SoyaAndPearl") != null
        // Add more tag checks as needed
        )
        {
            // There are existing game objects with the specified tags, so return and do not instantiate a new empty cup
            return;
        }

        Vector3 spawnPosition = new Vector3(1f, -3f, 0f);
        Quaternion spawnRotation = transform.rotation;

        Instantiate(emptyCupPrefab, spawnPosition, spawnRotation);
    }
}
