using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class winScript : MonoBehaviour
{
    
       

    private void Start()
    {
        
    }

    //[SerializeField] string sceneName;
    private void OnTriggerEnter(Collider collider)
    {

        
        if (collider.tag == Thief.TAG)
            Debug.Log("YOU WIN!!");
            WinSceneLoader();
                        
       
        
    }
    public void WinSceneLoader()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(5); // 5 = win

    }
}
