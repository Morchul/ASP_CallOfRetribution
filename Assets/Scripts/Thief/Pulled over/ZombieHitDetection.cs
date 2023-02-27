using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ZombieHitDetection : MonoBehaviour
{
    

    public Image healthBar;
    public float healthAmmount = 100;

    [SerializeField] GameObject zombieCollider;
  

    private void Update()
    {
        if (healthAmmount <= 0)
        {
            DeathSceneLoader();           
                      
        }
    }


    private void OnTriggerEnter(Collider zombie)
    {
        if (zombie.tag == "zombie")
        {
            Debug.Log("hit by zombie");
                    
                       
            TakeDamage(5);  
        }
    }

    public void TakeDamage(float damage)
    {

        healthAmmount -= damage;
        if (healthBar != null)
        {
            healthBar.fillAmount = healthAmmount / 100;
        }

    }

    public void DeathSceneLoader()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(3); 

    }


}
