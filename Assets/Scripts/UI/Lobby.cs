using UnityEngine;

public class Lobby : MonoBehaviour
{
    [SerializeField]
    private GameObject hostLobbyScreen;

    [SerializeField]
    private GameObject clientLobbyScreen;

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
    }
}
