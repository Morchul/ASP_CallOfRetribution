using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class shooting : MonoBehaviour
{

    [SerializeField] GameObject[] laser;
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem flash;
    [SerializeField] ParticleSystem cartridgeEject;
    [SerializeField] GameObject hitImpact;
    [SerializeField] TextMeshProUGUI hitDis;
    public AudioSource GunshotSource;
    public AudioClip Gunshot;
   
    int hitCon = 0; int i;
    private void Start()
    {
        i = 0;
    }
    void Update()
    {
        shoot();
    }
    void shoot()
    {
        bool i = Input.GetButton("Fire2");
        if (i)
        {
            SetActiveLaser(true);
            GunshotSource.PlayOneShot(Gunshot);
            ProcessRaycast();
        }
        else
        {
            SetActiveLaser(false);
        }
    }
    void SetActiveLaser(bool isActive)
    {
        foreach (GameObject lasers in laser)
        {
            var emissionMod = lasers.GetComponent<ParticleSystem>().emission;
            emissionMod.enabled = isActive;
            PlayFlash();
        }
    }
    private void PlayFlash()
    {
        flash.Play();
        cartridgeEject.Play();
    }
    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            hitCon++;
            enemyHealth target = hit.transform.GetComponent<enemyHealth>();
            if (target == null) { return; }
            target.TakeDamage(damage);

        }
    }
}
