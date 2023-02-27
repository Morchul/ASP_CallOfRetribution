using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class enemyHealth : MonoBehaviour
{
    public float hitPoints = 100f;
    public GameObject enemy;
       
        

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Debug.Log("You are dead");
            
            GameObject.Destroy(enemy);
            
        }
    }

    
    
}
