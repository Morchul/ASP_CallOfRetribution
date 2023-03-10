using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class waterTrigger : MonoBehaviour
{
    [SerializeField] string sceneName;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Thief.TAG))
            LoadScene();
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
