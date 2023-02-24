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

    //[Header("Controller")]
    //[SerializeField]
    //private SceneController sceneController;

    [SerializeField]
    private MainMenu mainMenu;

    [Header("Events")]
    [SerializeField]
    private GameEvent OnConnectionLost;
    [SerializeField]
    private GameEvent OnConnectionShutdown;

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
    }

    private void ReturnToMainMenu()
    {
        if (!host)
        {
            CloseLobby();
        }   
    }

    public void LeaveLobby()
    {
        NetworkManager.Instance.ConnectionHandler.Shutdown();
        if(host)
        {
            CloseLobby();
        }
    }

    private void CloseLobby()
    {
        OnConnectionLost.RemoveListener(ReturnToMainMenu);
        OnConnectionShutdown.RemoveListener(ReturnToMainMenu);

        //Disable lobby screen
        if (host)
            hostLobbyScreen.SetActive(false);
        else
            clientLobbyScreen.SetActive(false);

        mainMenu.Show();
    }
}
