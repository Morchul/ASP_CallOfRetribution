using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSound : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;
    private void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "Player")
        {
            if (audioSource != null)
            {
                audioSource.Play();              
            }
        }
    }
}
