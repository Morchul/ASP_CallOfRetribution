using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderDebug : MonoBehaviour
{
    [Header("Test values")]
    [SerializeField]
    private Mission testMission;

    [SerializeField]
    private Document doc;

    [SerializeField]
    private int bugID;

    [SerializeField]
    private IBugable.Type buggedType;

    [SerializeField]
    private int bugStatus;

    [SerializeField]
    private int radioMessageID;

    [Header("Events")]
    [SerializeField]
    private IntEvent OnNewInformation;
    [SerializeField]
    private IntEvent OnMissionSelect;
    [SerializeField]
    private BugUpdateEvent OnBugPlaced;
    [SerializeField]
    private IntEvent OnNewRadioMessage;

    [Header("Controller")]
    [SerializeField]
    private MissionController missionController;

    private void Awake()
    {
        missionController.Init();
        OnMissionSelect.RaiseEvent(testMission.ID);
    }

    public void ReleaseDocument()
    {
        OnNewInformation.RaiseEvent(doc.ID);
    }

    public void PlaceBug()
    {
        OnBugPlaced.RaiseEvent(bugID, buggedType, bugStatus);
    }

    public void SendImportantRadioMessage()
    {
        OnNewRadioMessage.RaiseEvent(radioMessageID);
    }
}
