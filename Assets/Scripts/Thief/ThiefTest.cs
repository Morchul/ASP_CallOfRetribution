using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefTest : PositionSensor
{
    [SerializeField]
    private MissionController missionController;

    [SerializeField]
    private Bug bugPrefab;

    [Header("Pos update")]
    [SerializeField]
    private float posUpdateInterval;
    private float posUpdateTimer;

    [Header("DEBUG")]
    [SerializeField]
    private ElectricalLock eLock;

    private Queue<Bug> bugs;

    private void Awake()
    {
        UpdateCreateFunc = MessageUtility.CreateThiefPosMessage;
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

    public void PlaceABug(IBugable bugable)
    {
        if(bugs.Count > 0)
        {
            Bug bug = bugs.Dequeue();
            bug.PlaceOn(bugable);
        }
        else
        {

        }
    }

    public void ReceiveABug(Bug bug)
    {
        bug.Remove();
        bugs.Enqueue(bug);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlaceABug(eLock);
        }


        if (posUpdateTimer > 0)
            posUpdateTimer -= Time.deltaTime;
        else
        {
            SendPosUpdate();
            posUpdateTimer = posUpdateInterval;
        }
    }
}
