using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : PositionSensor
{
    [SerializeField]
    private MissionController missionController;

    [SerializeField]
    private Bug bugPrefab;

    [Header("Pos update")]
    [SerializeField]
    private float posUpdateInterval;
    private float posUpdateTimer;

    private Queue<Bug> bugs;

    public const string TAG = "Player";
    public const char IDENTIFIER = 'T';

    private void Awake()
    {
        identifier = IDENTIFIER;
    }

    private void Start()
    {
        bugs = new Queue<Bug>(missionController.Bugs.Length);
        for(int i = 0; i < missionController.Bugs.Length; ++i)
        {
            Bug bug = Instantiate(bugPrefab, transform);
            bug.Init(i);
            bugs.Enqueue(bug);
        }
    }

    public Bug GetBug()
    {
        if(bugs.Count > 0)
        {
            return bugs.Dequeue();
        }
        else
        {
            return null;
        }
    }

    public void ReceiveABug(Bug bug)
    {
        bugs.Enqueue(bug);
    }

    void Update()
    {
        if (posUpdateTimer > 0)
            posUpdateTimer -= Time.deltaTime;
        else
        {
            SendPosUpdate();
            posUpdateTimer = posUpdateInterval;
        }
    }
}
