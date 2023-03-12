using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    private Selectable[] navigation;
    private int selected;
    private int navigationLength;

    [SerializeField]
    private MapData mapData;

    [SerializeField]
    private TMP_Text consoleLog;

    [SerializeField]
    private TMP_InputField commandInput;

    [Header("Events")]
    [SerializeField]
    private BoolEvent OnDroneConnectionStateChange;
    [SerializeField]
    private FloatEvent OnScanOnCooldown;
    [SerializeField]
    private IntEvent OnBugDisturbed;
    [SerializeField]
    private IntEvent OnBugDenied;
    [SerializeField]
    private Vector2Event OnDroneMoveEvent;
    [SerializeField]
    private GameEvent OnDrownScanEvent;
    [SerializeField]
    private GameEvent OnDrownFlareEvent;

    [Header("Hacker")]
    [SerializeField]
    private Hacker hacker;

    private bool droneConnected;

    private delegate bool HandleCommand(string command);

    private List<HandleCommand> commandHandler;

    //All lower case
    public const string MOVE_COMMAND = "move";
    public const string SCAN_COMMAND = "scan";
    public const string FLARE_COMMAND = "fire flare";

    public const string CLOSE_PORT_COMMAND = "close port";

    private void Awake()
    {
        OnDroneConnectionStateChange.AddListener(DroneStateChanged);
        OnScanOnCooldown.AddListener(ScanOnCooldown);
        OnBugDisturbed.AddListener((id) => AddLog("Bug " + id + " did not respond"));
        OnBugDenied.AddListener((id) => AddLog("Bug " + id + ": Access denied!"));
        droneConnected = true;

        commandHandler = new List<HandleCommand>() { CheckForPortDefense,  ExecuteDroneCommand};
    }
    public void SubmitCommand()
    {
        if (selected == navigation.Length)
        {
            foreach (HandleCommand handler in commandHandler)
                if (handler(commandInput.text))
                    break;

            commandInput.text = "";
            commandInput.ActivateInputField();
        }
    }

    #region Drone
    private void DroneStateChanged(bool disturbed)
    {
        droneConnected = !disturbed;
        if (droneConnected)
        {
            AddLog("Connection to drone");
        }
        else
        {
            AddLog("Lost connection to drone");
        }
    }

    private void ScanOnCooldown(float cooldown)
    {
        AddLog("Scan on cooldown: " + cooldown + " seconds");
    }

    private bool ExecuteDroneCommand(string command)
    {
        if (!droneConnected)
        {
            AddLog("No connection to drone");
            return false;
        }

        if (command.ToLower().StartsWith(MOVE_COMMAND))
        {
            string coordinates = command.Substring(command.IndexOf(' '));
            if (MessageUtility.TryConvertToCoordinates(coordinates, out Vector2 coord))
            {
                AddLog("Move drone to position: " + coord);
                OnDroneMoveEvent.RaiseEvent(coord);
            }
        }

        else if (command.ToLower() == SCAN_COMMAND)
        {
            AddLog("Start scanning");
            OnDrownScanEvent.RaiseEvent();
        }

        else if(command.ToLower() == FLARE_COMMAND)
        {
            AddLog("Fire flare");
            OnDrownFlareEvent.RaiseEvent();
        }
        else
        {
            AddLog("Unknown command: " + command + " read the computer manual for commands");
            return false;
        }
        return true;
    }
    #endregion

    #region Hacker attack
    private int attackedPort;
    private bool CheckForPortDefense(string command)
    {
        if (command.ToLower().StartsWith(CLOSE_PORT_COMMAND))
        {
            string portString = command.Substring(CLOSE_PORT_COMMAND.Length);
            if (int.TryParse(portString, out int port))
            {
                AddLog("Closing port " + port);
                if(attackedPort > 0 && attackedPort == port)
                {
                    hacker.HackDefused();
                }
                return true;
            }
        }
        return false;
    }
    public void HackAttack(Hacker.AttackParameter param)
    {
        attackedPort = param.Port;
        AddLog(param.Message);
    }
    #endregion

    public void AddLog(string log)
    {
        string text = consoleLog.text;
        
        if(consoleLog.textInfo.lineCount > 10)
        {
            text = text.Substring(text.IndexOf('\n') + 1);
        }

        consoleLog.text = text + "\n" + log;
    }

    #region Navigation
    public void SetNavigation(Selectable[] navigation)
    {
        this.navigation = navigation;
        selected = navigation.Length - 1;
        navigationLength = navigation.Length + 1;
        //SelectNext();
    }

    public void Enable()
    {
        Select(selected);
    }

    private void Select(int index)
    {
        if (index == navigation.Length) commandInput.Select();
        else
        {
            navigation[index].Select();
        }
        
        selected = index;
    }

    private bool IsSelectable(int index)
    {
        if (index == navigation.Length) return true;
        else return navigation[index].interactable;
    }

    public void SelectNext()
    {
        for (int i = 0; i < navigationLength; ++i)
        {
            int index = (selected + i + 1) % navigationLength;
            if (IsSelectable(index))
            {
                Select(index);
                break;
            }
        }
    }

    public void SelectPrevious()
    {
        for (int i = 0; i < navigationLength; ++i)
        {
            int index = (selected - i - 1);
            if (index < 0) index += navigationLength;
            if (IsSelectable(index))
            {
                Select(index);
                break;
            }
        }
    }
    #endregion
}
