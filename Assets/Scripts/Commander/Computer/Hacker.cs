using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    [SerializeField]
    private MissionController missionController;

    public int Level { get; set; }
    private float timer;
    private float nextHack;

    [SerializeField]
    private float minTimerOnLevel1;
    [SerializeField]
    private float maxTimerOnLevel1;

    [SerializeField]
    private int maxLevel;

    [SerializeField]
    private float hackingTime;

    [SerializeField]
    private Console console;

    [Header("Events")]
    [SerializeField]
    private BoolEvent OnMissionFinished;

    public const int HACKED_STATE = 1;
    private bool hackInProgress;

    private void Start()
    {
        Level = 0;

        foreach (BugReferenc bug in missionController.Bugs)
            bug.OnStatusChanged += OnBugStatusChanged;

        PrepareNextHack();
    }

    private void OnBugStatusChanged(int oldValue, int newValue)
    {
        if(((newValue & HACKED_STATE) > 0) && ((oldValue & HACKED_STATE) == 0))
        {
            NewItemHacked();
        }
    }

    private void PrepareNextHack()
    {
        nextHack = Random.Range(minTimerOnLevel1, maxTimerOnLevel1);
        timer = 0;
        hackInProgress = false;
        Debug.Log("next attack in " + nextHack);
    }

    private void NewItemHacked()
    {
        Level = Mathf.Min(maxLevel, Level + 1);
        Debug.Log("New item hacked increase level to: " + Level);
    }

    private void Update()
    {
        if (Level == 0) return;

        if (hackInProgress)
        {
            timer += Time.deltaTime;
            if(timer > hackingTime - ((Level - 1) * hackingTime / maxLevel))
            {
                OnMissionFinished.RaiseEvent(false);
            }
        }
        else
        {
            timer += Time.deltaTime * Level / 2;

            if (timer > nextHack)
            {
                StartHack();
            }
        }
        
    }

    private void StartHack()
    {
        Debug.Log("Start hack");
        hackInProgress = true;
        timer = 0;
        console.HackAttack(GetAttackParameter());
    }

    public struct AttackParameter
    {
        public string Message;
        public int Port;
    }

    private AttackParameter GetAttackParameter()
    {
        int port = Random.Range(10_000, 100_000);
        return new AttackParameter()
        {
            Port = port,
            Message = "Incoming attack on port " + port
        };
    }

    public void HackDefused()
    {
        Debug.Log("hack defused");
        PrepareNextHack();
    }
}
