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
    }

    private void ReturnToMainMenu()
    {
        if (!NetworkManager.Instance.ConnectionHandler.IsHost())
            sceneController.GoToMainMenu();
    }

    public void LeaveLobby()
    {
        NetworkManager.Instance.ConnectionHandler.Shutdown();
        sceneController.GoToMainMenu();
    }
}
