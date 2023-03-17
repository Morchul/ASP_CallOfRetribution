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

    [SerializeField]
    private GameObject selectLevelPanel;
    [SerializeField]
    private GameObject waitForPlayerText;

    [Header("Events")]
    [SerializeField]
    private GameEvent OnConnectionLost;
    [SerializeField]
    private GameEvent OnConnectionShutdown;
    [SerializeField]
    private IntEvent OnMissionSelect;
    [SerializeField]
    private GameEvent OnMissionSet;
    [SerializeField]
    private GameEvent OnConnectionEstablished;

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

            if (NetworkManager.Instance.IsConnected)
                PlayerConnected();
        }
        else
        {
            clientLobbyScreen.SetActive(true);
        }

        OnConnectionLost.AddListener(ReturnToMainMenu);
        OnConnectionShutdown.AddListener(ReturnToMainMenu);
        OnMissionSet.AddListener(MissionSetEvent);
        OnConnectionEstablished.AddListener(PlayerConnected);
    }

    private void ReturnToMainMenu()
    {
        if (!host)
        {
            NetworkManager.Instance.ResetInstance();
            CloseLobby();
        }
        else
        {
            waitForPlayerText.SetActive(true);
            selectLevelPanel.SetActive(false);
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

    private void PlayerConnected()
    {
        waitForPlayerText.SetActive(false);
        selectLevelPanel.SetActive(true);
    }

    public void SelectMission(Mission mission)
    {
        OnMissionSelect.RaiseEvent(mission.ID);
    }

    private void MissionSetEvent()
    {
        gameController.StartMission();
        CloseLobby();
    }

    private void CloseLobby()
    {
        OnConnectionLost.RemoveListener(ReturnToMainMenu);
        OnConnectionShutdown.RemoveListener(ReturnToMainMenu);
        OnMissionSet.RemoveListener(MissionSetEvent);
        OnConnectionEstablished.RemoveListener(PlayerConnected);

        //Disable lobby screen
        if (host)
            hostLobbyScreen.SetActive(false);
        else
            clientLobbyScreen.SetActive(false);

        mainMenu.Show();
    }
}
