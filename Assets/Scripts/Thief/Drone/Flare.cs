using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : MonoBehaviour
{
    private Rigidbody rigidBody;

    [SerializeField]
    private float lifeTime;
    private float timer;

    [SerializeField]
    private float initialImpulse;

    [SerializeField]
    private float startGravity;
    [SerializeField]
    private AnimationCurve gravityDrop;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rigidBody.AddForce(new Vector3(Random.Range(0, 0.2f), initialImpulse, Random.Range(0, 0.2f)), ForceMode.Impulse);
        timer = 0;
    }

    private void FixedUpdate()
    {
        if ((timer += Time.fixedDeltaTime) > lifeTime)
        {
            Destroy(gameObject);
        }

        rigidBody.AddForce(new Vector3(0, -startGravity * gravityDrop.Evaluate(timer), 0));
    }
}
