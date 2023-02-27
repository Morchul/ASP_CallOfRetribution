using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanResultObject : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;
    private float timer;

    public ScanResults Pool { get; set; }

    public void SetInaktiv()
    {
        gameObject.SetActive(false);
    }

    public void SetPosition(Vector2 pos)
    {
        gameObject.SetActive(true);
        timer = lifeTime;
        transform.localPosition = new Vector3(pos.x, 0, pos.y);
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Pool.Return(this);
        }
    }
}
