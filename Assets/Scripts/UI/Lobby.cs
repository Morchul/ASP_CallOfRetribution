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

    [Header("Controller")]
    [SerializeField]
    private SceneController sceneController;

    [Header("Events")]
    [SerializeField]
    private GameEvent OnConnectionLost;
    [SerializeField]
    private GameEvent OnConnectionShutdown;

    void Start()
    {
        if (NetworkManager.Instance.ConnectionHandler.IsHost())
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
    }

    private void ReturnToMainMenu()
    {
        if (!NetworkManager.Instance.ConnectionHandler.IsHost())
        {
            CloseLobby();
        }   
    }

    public void LeaveLobby()
    {
        NetworkManager.Instance.ConnectionHandler.Shutdown();
        if(NetworkManager.Instance.ConnectionHandler.IsHost())
        {
            CloseLobby();
        }
    }

    private void CloseLobby()
    {
        OnConnectionLost.RemoveListener(ReturnToMainMenu);
        OnConnectionShutdown.RemoveListener(ReturnToMainMenu);
        sceneController.GoToMainMenu();
    }
}
