using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockProgram : Program
{
    private BugReferenc bug;
    private Lock.LockState state;

    [SerializeField]
    private List<Selectable> uiElements;

    [SerializeField]
    private TMP_Text lockState;

    private Console console;
    private MessageTransmitter transmitter;

    protected override void Awake()
    {
        base.Awake();
        navigation = uiElements;
        console = computer.Console;

        gameObject.SetActive(false);
    }

    private void Start()
    {
        transmitter = NetworkManager.Instance.Transmitter;
    }

    public void SetBugReferenc(BugReferenc bug)
    {
        this.bug = bug;
        state = (Lock.LockState)bug.Status;

        bug.OnStatusChanged += BugStatusChanged;
        bug.OnTypeChanged += CloseProgram;

        UpdateUI();
    }

    public void BugStatusChanged(int oldState, int newState)
    {
        state = (Lock.LockState)newState;

        int open = (newState & (int)Lock.LockState.Open);
        string stateText = "Lock state: " + ((open == 0) ? "Closed" : "Open");

        if ((newState & (int)Lock.LockState.Hacked) != 0 && (oldState & (int)Lock.LockState.Hacked) == 0)
            console.AddLog("Access gained");
        if(open != (oldState & (int)Lock.LockState.Open))
        {
            console.AddLog(stateText);
        }

        lockState.text = stateText;
    }

    private void UpdateUI()
    {
        string stateText = ((state & Lock.LockState.Open) == 0) ? "Closed" : "Open";
        lockState.text = "Lock state: " + stateText;
    }

    public void Hack()
    {
        StartCoroutine(HackLock());
    }

    public void LockAction()
    {
        StartCoroutine(CloseLock());
    }

    public void Unlock()
    {
        StartCoroutine(OpenLock());
    }

    public IEnumerator HackLock()
    {
        console.AddLog("Getting access to lock...");
        yield return new WaitForSeconds(1);
        for(int i = 0; i < 3; ++i)
        {
            console.AddLog("Hacking...");
            yield return new WaitForSeconds(1);
        }

        transmitter.WriteToHost(MessageUtility.CreateBugUpdateMessage(bug.ID, bug.Type, (int)(state | Lock.LockState.Hacked)));
    }

    private IEnumerator CloseLock()
    {
        console.AddLog("Closing lock...");
        
        if ((state & Lock.LockState.Hacked) == 0)
        {
            yield return new WaitForSeconds(1);
            console.AddLog("Access denied!");
        }
        else
        {
            transmitter.WriteToHost(MessageUtility.CreateBugUpdateMessage(bug.ID, bug.Type, (int)(state & ~Lock.LockState.Open)));
        }
            
    }

    private IEnumerator OpenLock()
    {
        console.AddLog("Opening lock...");

        if ((state & Lock.LockState.Hacked) == 0)
        {
            yield return new WaitForSeconds(1);
            console.AddLog("Access denied!");
        }
        else
        {
            transmitter.WriteToHost(MessageUtility.CreateBugUpdateMessage(bug.ID, bug.Type, (int)(state | Lock.LockState.Open)));
        }
    }

    public override void CloseProgram()
    {
        bug.OnStatusChanged -= BugStatusChanged;
        bug.OnTypeChanged -= CloseProgram;
        bug = null;
        base.CloseProgram();
        computer.ReturnToBugList();
    }
}
