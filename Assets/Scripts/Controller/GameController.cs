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
    private BoolEvent OnMissionFinished;

    [SerializeField]
    private GameEvent OnMissionLoaded;
    [SerializeField]
    private GameEvent OnGameReady;

    private int clientsReady;

    public bool InputDisabled { get; private set; }

    public void Init()
    {
        OnConnectionLost.AddListener(ReturnToLobby);
        OnConnectionShutdown.AddListener(ReturnToLobby);
        OnMissionFinished.AddListener(MissionFinished);
        OnMissionLoaded.AddListener(MissionLoaded);
        clientsReady = 0;
        InputDisabled = false;
    }

    private void MissionFinished(bool successful)
    {
        StopGame();
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
        if (++clientsReady == 2 || NetworkManager.Instance.DEBUG_MODE)
        {
            InputDisabled = false;
            OnGameReady.RaiseEvent();
        }  
    }

    public void StartMission()
    {
        InputDisabled = true;
        if (missionController.CurrentMission != null)
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

    public void MenuActive(bool active)
    {
        InputDisabled = active;
    }

    public void ReturnToLobby()
    {
        if (missionController.CurrentMission == null) return;
        ResumeGame();
        OnResetGame.RaiseEvent();
        sceneController.GoToMainMenu();
    }

    public void LeaveGame()
    {
        NetworkManager.Instance.Shutdown();
        ReturnToLobby();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
