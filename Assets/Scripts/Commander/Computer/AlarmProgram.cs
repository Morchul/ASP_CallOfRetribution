using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlarmProgram : Program
{
    private BugReferenc bug;
    private Alarm.AlarmState state;

    [SerializeField]
    private List<Selectable> uiElements;

    [SerializeField]
    private TMP_Text alarmState;

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
        state = (Alarm.AlarmState)bug.Status;

        bug.OnStatusChanged += BugStatusChanged;
        bug.OnTypeChanged += CloseProgram;

        UpdateUI();
    }

    public void BugStatusChanged(int oldState, int newState)
    {
        state = (Alarm.AlarmState)newState;

        int disabled = (newState & (int)Alarm.AlarmState.Disabled);
        string stateText = "Alarm state: " + ((disabled == 0) ? "Enabled" : "Disabled");

        if ((newState & (int)Alarm.AlarmState.Hacked) != 0 && (oldState & (int)Alarm.AlarmState.Hacked) == 0)
            StartCoroutine(HackAlarm());

        if (disabled != (oldState & (int)Alarm.AlarmState.Disabled))
        {
            console.AddLog(stateText);
        }

        alarmState.text = stateText;
    }

    private void UpdateUI()
    {
        string stateText = ((state & Alarm.AlarmState.Disabled) == 0) ? "Enabled" : "Disabled";
        alarmState.text = "Lock state: " + stateText;
    }

    public void Hack()
    {
        console.AddLog("Getting access to lock...");
        transmitter.WriteToHost(MessageUtility.CreateBugUpdateMessage(bug.ID, bug.Type, (int)(state | Alarm.AlarmState.Hacked)));
        StartCoroutine(HackAlarm());
    }

    public void Enable()
    {
        if (computer.Busy) return;
        StartCoroutine(EnableAlarm());
    }

    public void Disable()
    {
        if (computer.Busy) return;
        StartCoroutine(DisableAlarm());
    }

    public void SetOff()
    {
        if (computer.Busy) return;
        console.AddLog("Set of alarm");
        transmitter.WriteToHost(MessageUtility.CreateBugUpdateMessage(bug.ID, bug.Type, (int)(state | Alarm.AlarmState.On)));
    }

    private IEnumerator HackAlarm()
    {
        computer.Busy = true;
        for (int i = 0; i < 3; ++i)
        {
            console.AddLog("Hacking...");
            yield return new WaitForSeconds(1);
        }

        console.AddLog("Access gained");
        computer.Busy = false;
    }

    private IEnumerator EnableAlarm()
    {
        console.AddLog("Enable alarm...");

        yield return new WaitForSeconds(1);
        transmitter.WriteToHost(MessageUtility.CreateBugUpdateMessage(bug.ID, bug.Type, (int)(state & ~Alarm.AlarmState.Disabled)));
    }

    private IEnumerator DisableAlarm()
    {
        console.AddLog("Disable alarm...");
        yield return new WaitForSeconds(1);
        transmitter.WriteToHost(MessageUtility.CreateBugUpdateMessage(bug.ID, bug.Type, (int)(state | Alarm.AlarmState.Disabled)));
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
