using UnityEngine;

public class SyrupSpawner : MonoBehaviour
{
    public GameObject syrupScoopPrefab; // Reference to the syrupScoop prefab in the Inspector.

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast from the mouse position into the scene.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null)
            {
                // Check if a 2D collider was hit.
                if (hit.collider.CompareTag("SyrupCollider")) // Replace "SyrupCollider" with your collider's tag.
                {
                    // Instantiate the syrupScoop prefab at the hit point.
                    Vector3 spawnPosition = hit.point;
                    spawnPosition.z = 0f;
                    Instantiate(syrupScoopPrefab, spawnPosition, Quaternion.identity);
                }
            }
        }
    }
}
