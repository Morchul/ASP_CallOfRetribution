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
        transmitter = NetworkManager.Instance.Transmitter;

        gameObject.SetActive(false);
    }

    public void SetBugReferenc(BugReferenc bug)
    {
        this.bug = bug;
        
        bug.OnStatusChanged += UpdateUI;
        bug.OnTypeChanged += CloseProgram;

        UpdateUI();
    }

    private void UpdateUI()
    {
        state = (Lock.LockState)bug.Status;
        string stateText = ((state & Lock.LockState.Open) == 0) ? "Locked" : "Unlocked";
        lockState.text = "Lock state: " + stateText;
        console.AddLog("Bug state: " + stateText);
    }

    public void Hack()
    {
        StartCoroutine(HackLock());
    }

    public IEnumerator HackLock()
    {
        console.AddLog("Start get access to lock...");
        yield return new WaitForSeconds(1);
        for(int i = 0; i < 3; ++i)
        {
            console.AddLog("Hacking...");
            yield return new WaitForSeconds(1);
        }

        console.AddLog("Access gained!");
        transmitter.WriteToHost(MessageUtility.CreateBugUpdateMessage(bug.ID, bug.Type, (int)(state | Lock.LockState.Hacked)));
    }

    public void LockAction()
    {
        if ((state & Lock.LockState.Hacked) == 0)
            console.AddLog("Access denied!");
        else
            transmitter.WriteToHost(MessageUtility.CreateBugUpdateMessage(bug.ID, bug.Type, (int)(state & ~Lock.LockState.Open)));
    }

    public void Unlock()
    {
        if ((state & Lock.LockState.Hacked) == 0)
            console.AddLog("Access denied!");
        else
            transmitter.WriteToHost(MessageUtility.CreateBugUpdateMessage(bug.ID, bug.Type, (int)(state | Lock.LockState.Open)));
    }

    public override void CloseProgram()
    {
        bug.OnStatusChanged -= UpdateUI;
        bug.OnTypeChanged -= CloseProgram;
        bug = null;
        base.CloseProgram();
        computer.ReturnToBugList();
    }
}
