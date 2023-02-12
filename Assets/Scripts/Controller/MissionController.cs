using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionController", menuName = "Controller/MissionController")]
public class MissionController : ScriptableObject
{
    private List<int> currentAvailableInformation;

    [Header("Events")]
    public IntEvent OnNewInformation;
    public IntEvent OnMissionSelect;
    public GameEvent OnMissionLoaded;

    public Mission CurrentMission { get; private set; }

    [SerializeField]
    private Mission[] missions;

    public void Init()
    {
        OnMissionSelect.AddListener(SetMission);
        OnMissionLoaded.AddListener(StartMission);

        currentAvailableInformation = new List<int>();
    }

    private void StartMission()
    {
        CurrentMission.Start();
    }

    private void SetMission(int missionID)
    {
        CurrentMission = missions.First((mission) => mission.ID == missionID);
        OnNewInformation.AddListener(NewInformation);
    }

    public void FinishMission()
    {
        CurrentMission = null;
        currentAvailableInformation.Clear();
        OnNewInformation.RemoveListener(NewInformation);
    }

    private void NewInformation(int infoID)
    {
        currentAvailableInformation.Add(infoID);
    }
}
