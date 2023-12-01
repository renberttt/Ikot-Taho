using UnityEngine;

public class MoveObject : MonoBehaviour
{
   
    public float targetX = -0.48f;

   
    public float moveSpeed = 5.0f;

    void Update()
    {
        
        Vector3 currentPosition = transform.position;

        
        Vector3 targetPosition = new Vector3(targetX, currentPosition.y, currentPosition.z);

   
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
    }
}
