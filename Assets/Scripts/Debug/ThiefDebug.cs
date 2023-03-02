using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefDebug : MonoBehaviour
{
    [Header("Test values")]
    [SerializeField]
    private Mission testMission;

    [Header("Events")]
    [SerializeField]
    private IntEvent OnMissionSelect;

    [Header("Controller")]
    [SerializeField]
    private MissionController missionController;

    private void Awake()
    {
        missionController.Init();
        OnMissionSelect.RaiseEvent(testMission.ID);
    }
}
