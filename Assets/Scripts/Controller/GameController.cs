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

    public void Init()
    {
        OnConnectionLost.AddListener(ReturnToLobby);
        OnConnectionShutdown.AddListener(ReturnToLobby);
        OnMissionFinishedSuccessfully.AddListener(StopGame);
        OnMissionFailed.AddListener(StopGame);
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
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

    public void SelectMission(Mission mission)
    {
        NetworkManager.Instance.Transmitter.WriteToHost(MessageUtility.CreateSelectMissionMessage(mission.ID));
    }

    public void ReturnToLobby()
    {
        if (missionController.CurrentMission == null) return;
        ResumeGame();
        OnResetGame.RaiseEvent();
        sceneController.GoToMainMenu();
    }
}
