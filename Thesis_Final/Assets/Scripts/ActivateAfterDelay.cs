using UnityEngine;

public class ActivateAfterDelay : MonoBehaviour
{

    void Start()
    {
    
        gameObject.SetActive(false);

        
        Invoke("EnableObject", 3f);
    }

  
    void EnableObject()
    {
        
        gameObject.SetActive(true);
    }
}
