using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GuardNPC : MonoBehaviour
{
    // Declare variables
    public float suspicionLevel;
    public float fieldOfView;
    public Transform target;
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public bool patrolling;
    public int currentWaypoint;


    // Start is called before the first frame update
    void Start()
    {
        suspicionLevel = 0f;
        fieldOfView = 180f;
        navMeshAgent = GetComponent<NavMeshAgent>();
        patrolling = true;
        currentWaypoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (InSight())
        {
            suspicionLevel += 0.1f;
            if (suspicionLevel >= 10)
            {
                FollowTarget();
                patrolling = false;
            }
        }
        else
        {
            suspicionLevel = 0f;
            patrolling = true;
        }

/*        // Patrol if the NPC is not following the target using NavMesh
        if (navMeshAgent.destination == null && patrolling == true)
        {
            // Move to the next waypoint
            navMeshAgent.destination = waypoints[currentWaypoint].position;
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
*/

        // Patrol if the NPC is not following the target using Linear Interpolation (Lerp)
        if (patrolling == true && !navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                // Lerp (Linear interpolation) to the next waypoint
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
                navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
            }
        }
    }

    // Check if the target is in sight
    bool InSight()
    {
        // Calculate the angle between the target and the NPC
        Vector3 direction = target.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);


        // Check if the target is within the field of view
        if (angle < fieldOfView * 0.5f)
        {
            // Check if there is an obstacle between the target and the NPC
            Vector3 directionUp = target.position - (transform.position + transform.up);
            float angleUp = Vector3.Angle(directionUp, transform.forward);
            Vector3 directionDown = target.position - (transform.position - transform.up);
            float angleDown = Vector3.Angle(directionDown, transform.forward);

            // Check the angles in all three directions
            if (angle < fieldOfView * 0.5f && angleUp < fieldOfView * 0.5f && angleDown < fieldOfView * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction.normalized, out hit, fieldOfView))
                {
                    if (hit.transform == target)
                    {
                        return true;
                    }
                }
            }
        }
        return false;

    }
    // Follow the target
    void FollowTarget()
    {
        // NavMeshAgent to follow target while avoiding obstacles
        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.destination = target.position;
    }

}

