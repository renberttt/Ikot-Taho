using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
  
    public string sceneToLoad = "Credits";

   
    void Start()
    {
        
        Invoke("LoadScene", 12f);
    }

   
    void LoadScene()
    {
    
        SceneManager.LoadScene(sceneToLoad);
    }
}
