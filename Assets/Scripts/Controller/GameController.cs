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
        NetworkManager.Instance.Transmitter.WriteToHost(MessageHandler.SELECT_MISSION_PREFIX + mission.ID);
    }
}
