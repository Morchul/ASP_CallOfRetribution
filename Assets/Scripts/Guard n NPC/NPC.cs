using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    public float wanderRadius;


    void Awake()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.velocity == Vector3.zero)
        {
            // Generate a random direction
            Vector3 newDirection = Random.insideUnitSphere * wanderRadius;
            newDirection += transform.position;
            UnityEngine.AI.NavMeshHit hit;
            UnityEngine.AI.NavMesh.SamplePosition(newDirection, out hit, wanderRadius, 1);
            Vector3 finalPosition = hit.position;
            navMeshAgent.SetDestination(finalPosition);
        }
    }

}
