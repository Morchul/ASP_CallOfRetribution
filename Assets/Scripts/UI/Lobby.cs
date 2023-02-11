using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Lobby : MonoBehaviour
{
    [SerializeField]
    private GameObject hostLobbyScreen;

    [SerializeField]
    private GameObject clientLobbyScreen;

    [SerializeField]
    [Scene]
    private string mainMenu;

    [SerializeField]
    private GameEvent OnConnectionLost;

    [SerializeField]
    private TextMeshProUGUI ipAddress;

    void Start()
    {
        if(NetworkManager.Instance.ConnectionHandler.IsHost())
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
            SceneManager.LoadScene(mainMenu);
    }
}
