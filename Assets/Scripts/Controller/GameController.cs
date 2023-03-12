using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameController", menuName = "Controller/GameController")]
public class GameController : ScriptableObject
{
    [SerializeField]
    private SceneController sceneController;

    [SerializeField]
    private MissionController missionController;

    [SerializeField]
    private GameEvent OnResetGame;

    [SerializeField]
    private GameEvent OnConnectionLost;
    [SerializeField]
    private GameEvent OnConnectionShutdown;
    [SerializeField]
    private GameEvent OnMissionFinishedSuccessfully;
    [SerializeField]
    private GameEvent OnMissionFailed;
    [SerializeField]
    private IntEvent OnMissionSelected;

    [SerializeField]
    private GameEvent OnMissionLoaded;
    [SerializeField]
    private GameEvent OnGameReady;

    private int clientsReady;

    public void Init()
    {
        OnConnectionLost.AddListener(ReturnToLobby);
        OnConnectionShutdown.AddListener(ReturnToLobby);
        OnMissionFinishedSuccessfully.AddListener(StopGame);
        OnMissionFailed.AddListener(StopGame);
        OnMissionLoaded.AddListener(MissionLoaded);
        clientsReady = 0;
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private void MissionLoaded()
    {
        if (++clientsReady == 2)
            OnGameReady.RaiseEvent();
    }

    public void StartMission()
    {
        if(missionController.CurrentMission != null)
        {
            if (NetworkManager.Instance.ConnectionHandler.IsHost())
                sceneController.LoadMissionForThief(missionController.CurrentMission);
            else
                sceneController.LoadCommanderView(true);
        }
        else
        {
            Debug.LogError("Can't start game, no mission is selected!");
        }
    }

    public void ReturnToLobby()
    {
        if (missionController.CurrentMission == null) return;
        ResumeGame();
        OnResetGame.RaiseEvent();
        sceneController.GoToMainMenu();
    }
}
