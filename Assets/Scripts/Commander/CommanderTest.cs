using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderTest : MonoBehaviour
{
    [SerializeField]
    private Information info;

    [SerializeField]
    private IntEvent OnNewInformation;
    [SerializeField]
    private IntEvent OnMissionSelect;

    [SerializeField]
    private Mission testMission;

    [SerializeField]
    private MissionController missionController;

    private void Awake()
    {
        missionController.Init();
        OnMissionSelect.RaiseEvent(testMission.ID);
    }

    public void ReleaseInformation()
    {
        OnNewInformation.RaiseEvent(info.ID);
    }
}
