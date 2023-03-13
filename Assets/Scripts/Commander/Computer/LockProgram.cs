using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockProgram : Program
{
    private BugReferenc bug;
    private ElectricalLock.LockState state;

    [SerializeField]
    private List<Selectable> uiElements;

    [SerializeField]
    private TMP_Text lockState;

    private Console console;

    [SerializeField]
    private BugUpdateEvent OnBugUpdateRequestEvent;

    protected override void Awake()
    {
        base.Awake();
        navigation = uiElements;
        console = computer.Console;

        gameObject.SetActive(false);
    }

    public void SetBugReferenc(BugReferenc bug)
    {
        this.bug = bug;
        state = (ElectricalLock.LockState)bug.Status;

        bug.OnStatusChanged += BugStatusChanged;
        bug.OnTypeChanged += CloseProgram;

        UpdateUI();
    }

    public void BugStatusChanged(int oldState, int newState)
    {
        state = (ElectricalLock.LockState)newState;

        int open = (newState & (int)ElectricalLock.LockState.Open);
        string stateText = "Lock state: " + ((open == 0) ? "Closed" : "Open");

        if ((newState & (int)ElectricalLock.LockState.Hacked) != 0 && (oldState & (int)ElectricalLock.LockState.Hacked) == 0)
            StartCoroutine(HackLock());
        
        if(open != (oldState & (int)ElectricalLock.LockState.Open))
        {
            console.AddLog(stateText);
        }

        lockState.text = stateText;
    }

    private void UpdateUI()
    {
        string stateText = ((state & ElectricalLock.LockState.Open) == 0) ? "Closed" : "Open";
        lockState.text = "Lock state: " + stateText;
    }

    public void Hack()
    {
        console.AddLog("Getting access to lock...");
        OnBugUpdateRequestEvent.RaiseEvent(bug.ID, bug.Type, (int)(state | ElectricalLock.LockState.Hacked));
    }

    public void LockAction()
    {
        if (computer.Busy) return;
        StartCoroutine(CloseLock());
    }

    public void Unlock()
    {
        if (computer.Busy) return;
        StartCoroutine(OpenLock());
    }

    private IEnumerator HackLock()
    {
        computer.Busy = true;
        for(int i = 0; i < 3; ++i)
        {
            console.AddLog("Hacking...");
            yield return new WaitForSeconds(1);
        }

        console.AddLog("Access gained");
        computer.Busy = false;
    }

    private IEnumerator CloseLock()
    {
        console.AddLog("Closing lock...");

        yield return new WaitForSeconds(1);
        OnBugUpdateRequestEvent.RaiseEvent(bug.ID, bug.Type, (int)(state & ~ElectricalLock.LockState.Open));
    }

    private IEnumerator OpenLock()
    {
        console.AddLog("Opening lock...");
        yield return new WaitForSeconds(1);
        OnBugUpdateRequestEvent.RaiseEvent(bug.ID, bug.Type, (int)(state | ElectricalLock.LockState.Open));
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
