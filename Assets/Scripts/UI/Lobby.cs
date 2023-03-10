using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Lobby : MonoBehaviour
{
    [Header("UI elements")]
    [SerializeField]
    private GameObject hostLobbyScreen;

    [SerializeField]
    private GameObject clientLobbyScreen;

    [SerializeField]
    private TextMeshProUGUI ipAddress;

    [SerializeField]
    private MainMenu mainMenu;

    [Header("Events")]
    [SerializeField]
    private GameEvent OnConnectionLost;
    [SerializeField]
    private GameEvent OnConnectionShutdown;
    [SerializeField]
    private IntEvent OnMissionSelect;

    [Header("Controller")]
    [SerializeField]
    private GameController gameController;

    private bool host;

    public void ShowLobby(bool host)
    {
        this.host = host;
        if (host)
        {
            hostLobbyScreen.SetActive(true);
            ipAddress.text = NetworkManager.Instance.ConnectionHandler.IP;
        }
        else
        {
            clientLobbyScreen.SetActive(true);
        }

        OnConnectionLost.AddListener(ReturnToMainMenu);
        OnConnectionShutdown.AddListener(ReturnToMainMenu);
        OnMissionSelect.AddListener(MissionSelectEvent);
    }

    private void ReturnToMainMenu()
    {
        if (!host)
        {
            NetworkManager.Instance.ResetInstance();
            CloseLobby();
        }
    }

    public void LeaveLobby()
    {
        NetworkManager.Instance.ConnectionHandler.Shutdown();
        NetworkManager.Instance.ResetInstance();
        if(host)
        {
            CloseLobby();
        }
    }

    public void SelectMission(Mission mission)
    {
        OnMissionSelect.RaiseEvent(mission.ID);
        gameController.StartMission();
    }

    private void MissionSelectEvent(int _)
    {
        CloseLobby();
    }

    private void CloseLobby()
    {
        OnConnectionLost.RemoveListener(ReturnToMainMenu);
        OnConnectionShutdown.RemoveListener(ReturnToMainMenu);
        OnMissionSelect.RemoveListener(MissionSelectEvent);

        //Disable lobby screen
        if (host)
            hostLobbyScreen.SetActive(false);
        else
            clientLobbyScreen.SetActive(false);

        mainMenu.Show();
    }
}
