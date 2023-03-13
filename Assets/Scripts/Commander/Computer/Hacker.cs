using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    [SerializeField]
    private MissionController missionController;

    private int level = 0;
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
        level = Mathf.Min(maxLevel, level + 1);
        Debug.Log("New item hacked increase level to: " + level);
    }

    private void Update()
    {
        if (level == 0) return;

        if (hackInProgress)
        {
            timer += Time.deltaTime;
            if(timer > hackingTime - ((level - 1) * hackingTime / maxLevel))
            {
                OnMissionFinished.RaiseEvent(false);
            }
        }
        else
        {
            timer += Time.deltaTime * level / 2;

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
        int port = Random.Range(1000, 9999);
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
