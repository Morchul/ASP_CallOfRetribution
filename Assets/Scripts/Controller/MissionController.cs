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
    public BugUpdateEvent OnBugUpdate;
    public GameEvent OnMissionFinishedSuccessfully;
    public GameEvent OnResetGame;

    public Mission CurrentMission { get; private set; }

    [Header("")]
    [SerializeField]
    private Mission[] missions;

    [SerializeField]
    private ProgressController progressController;

    public BugReferenc[] Bugs;

    public void Init()
    {
        OnMissionSelect.AddListener(SetMission);
        OnMissionLoaded.AddListener(StartMission);
        OnResetGame.AddListener(() => CurrentMission = null);
        OnMissionFinishedSuccessfully.AddListener(UpdateMissionProgress);

        if (currentAvailableInformation == null)
            currentAvailableInformation = new List<int>();
        else
            currentAvailableInformation.Clear();
    }

    private void StartMission()
    {
        CurrentMission.ShowStartDocuments();
    }

    private void SetMission(int missionID)
    {
        CurrentMission = missions.First((mission) => mission.ID == missionID);
        OnNewInformation.AddListener(NewInformation);

        Bugs = new BugReferenc[CurrentMission.AmountOfBugs];
        for (int i = 0; i < CurrentMission.AmountOfBugs; ++i)
        {
            Bugs[i] = new BugReferenc(i);
        }
        OnBugUpdate.AddListener(BugUpdate);
    }

    private void UpdateMissionProgress()
    {
        progressController.SetCurrentProgress(CurrentMission.ID);
    }

    private void BugUpdate(int bugID, IBugable.Type type, int status)
    {
        Bugs[bugID].Update(type, status);
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
