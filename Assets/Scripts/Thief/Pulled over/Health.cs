using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public Image healthBar;
    public float healthAmmount = 100;

    public void TakeDamage(float damage)
    {
       
        healthAmmount -= damage;
        if (healthBar != null) {
            healthBar.fillAmount = healthAmmount / 100; }
        
    }

    

    
}
