using UnityEngine;

public class PowerUps : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Replace "ChildObjectName" with the name of your child GameObject
        Transform childTransform = transform.Find("ChildObjectName");

        if (childTransform != null)
        {
            string childName = childTransform.gameObject.name;
            Debug.Log("Selected Child Name: " + childName);
        }
        else
        {
            Debug.Log("Child GameObject not found!");
        }
    }
}
