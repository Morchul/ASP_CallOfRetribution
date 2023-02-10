using UnityEngine;
using UnityEngine.SceneManagement;

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

    void Start()
    {
        if(NetworkManager.Instance.ConnectionHandler.IsHost())
        {
            hostLobbyScreen.SetActive(true);
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
