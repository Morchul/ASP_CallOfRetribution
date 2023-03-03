using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionDoor : MonoBehaviour
{
    public Collider playerCollider;
    public string levelToLoad;


    // Update is called once per frame
    void Update()
    {
        if (playerCollider.bounds.Intersects(this.GetComponent<Collider>().bounds))
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log("Loading Level");
                UnityEngine.SceneManagement.SceneManager.LoadScene(levelToLoad);
            }
        }
    }

    void OnGUI()
    {
        OnCollision();
    }
    void OnCollision()
    {
        if (playerCollider.bounds.Intersects(this.GetComponent<Collider>().bounds))
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 20), "Press E to enter");
        }
    }
}