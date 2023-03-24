using UnityEngine;

public class CommanderDebug : MonoBehaviour
{
    [Header("Test values")]
    [SerializeField]
    private Mission testMission;

    [Header("Events")]
    [SerializeField]
    private IntEvent OnMissionSelect;
    [SerializeField]
    private GameEvent OnMissionLoaded;
    [SerializeField]
    private GameEvent OnGameReady;

    [Header("Controller")]
    [SerializeField]
    private MissionController missionController;

    private void Awake()
    {
        missionController.Init();
        OnMissionSelect.RaiseEvent(testMission.ID);
    }

    private void Start()
    {
        OnMissionLoaded.RaiseEvent();
        OnGameReady.RaiseEvent();
    }
}
