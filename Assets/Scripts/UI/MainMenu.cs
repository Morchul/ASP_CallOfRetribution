using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private IPAddressField ipAddressField;

    [SerializeField]
    private GameEvent OnConnectionRefused;

    [SerializeField]
    private GameEvent OnConnectionEstablished;

    [SerializeField]
    private GameEvent OnConnectionFailure;

    [SerializeField]
    private TextMeshProUGUI messageText;

    [SerializeField]
    [Scene]
    private string lobbyScene;

    private void Start()
    {
        OnConnectionRefused.AddListener(ConnectionRefused);
        OnConnectionEstablished.AddListener(ConnectionEstablished);
        OnConnectionFailure.AddListener(ConnectionFailure);
    }

    private void ConnectionRefused()
    {
        messageText.text = "Connection refused";
    }

    private void ConnectionFailure()
    {
        messageText.text = "Could not connect!";
    }

    private void ConnectionEstablished()
    {
        if (!NetworkManager.Instance.ConnectionHandler.IsHost())
        {
            Debug.Log("Join lobby as Client");
            SceneManager.LoadScene(lobbyScene);
            //Join lobby as client
        }
    }

    public void HostGame()
    {
        NetworkManager.Instance.CreateHost();
        if (NetworkManager.Instance.ConnectionHandler.Running)
        {
            Debug.Log("Join lobby as host");
            SceneManager.LoadScene(lobbyScene);
            //Join lobby as host
        }
    }

    public void JoinGame()
    {
        if (ipAddressField.Valid)
        {
            messageText.text = "Try to connect ...";
            StartCoroutine(JoinGameWithDelay());
        }
        else
        {
            messageText.text = "Invalid IP address";
        }
    }

    private IEnumerator JoinGameWithDelay()
    {
        yield return new WaitForSeconds(1);
        NetworkManager.Instance.CreateClient(ipAddressField.IP_Address);
    }
}
