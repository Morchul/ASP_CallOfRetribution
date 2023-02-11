using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Net;

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
    private TMP_Dropdown ipSelection;

    [SerializeField]
    [Scene]
    private string lobbyScene;

    private void Start()
    {
        OnConnectionRefused.AddListener(ConnectionRefused);
        OnConnectionEstablished.AddListener(ConnectionEstablished);
        OnConnectionFailure.AddListener(ConnectionFailure);

        ipSelection.options.Clear();
        
        string strHostName = Dns.GetHostName();
        IPHostEntry host = Dns.GetHostEntry(strHostName);

        foreach(IPAddress ipA in host.AddressList)
        {
            ipSelection.options.Add(new TMP_Dropdown.OptionData(ipA.ToString()));
        }

        if(ipSelection.options.Count > 1)
            ipSelection.value = 1;
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
        NetworkManager.Instance.CreateHost(ipSelection.options[ipSelection.value].text);
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
