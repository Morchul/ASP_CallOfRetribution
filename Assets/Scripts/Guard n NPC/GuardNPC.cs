using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GuardNPC : MonoBehaviour
{
    // Declare variables
    public float suspicionLevel;
    public int suspicionTime;
    public float fieldOfView;
    public Transform target;
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public bool patrolling;
    public int currentWaypoint;
    public float sightRange;
    public Transform player;
    public float followSpeed = 5f;
    public bool isFollowing = false;
    private bool hasStolen = false;

    [Header("Events")]
    [SerializeField]
    private GameEvent OnSuspiciousActionExecuted;
    [SerializeField]
    private GameEvent OnItemStolden;

    private void Awake()
    {
        OnSuspiciousActionExecuted.AddListener(OnSuspiciousAction);
        OnItemStolden.AddListener(ItemStolen);
    }

    // Start is called before the first frame update
    void Start()
    {
        suspicionLevel = 0f;
        suspicionTime = 10;
        fieldOfView = 180f;
        navMeshAgent = GetComponent<NavMeshAgent>();
        patrolling = true;
        currentWaypoint = 0;
        sightRange = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (InSight())
        {
            suspicionLevel += Time.deltaTime;
            if (suspicionLevel >= suspicionTime)
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
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
                navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
        }
        if (hasStolen == true)
        {
            Vector3 targetPosition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }

        /*        // Patrol if the NPC is not following the target
                if (navMeshAgent.destination == null && patrolling == true)

        // Patrol if the NPC is not following the target
        if (patrolling == true && !navMeshAgent.pathPending)
        {
            // Check to see if the player is within the sight range of the guard
            if (Vector3.Distance(target.transform.position, transform.position) < sightRange)
            {
                navMeshAgent.destination = target.transform.position;
            }
            else
            {
                // Move to the next waypoint
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
                navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
            }
        }*/
    }
    // Check if the target is in sight
    bool InSight()
    {
        // Calculate the angle between the target and the NPC
        Vector3 direction = target.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        // Check to see if the player is within the sight range of the guard
        if (Vector3.Distance(target.transform.position, transform.position) < sightRange)
        {
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
    void OnTriggerStay(Collider other)
    {
        if (other.tag == Thief.TAG)
        {
            hasStolen = true;
        }
    }

    public void AlarmOnPosition(Vector3 alarmPosition)
    {
        // TODO
    }

    public void OnSuspiciousAction()
    {
        // TODO
    }
    public void ItemStolen()
    {
        
    }
}

