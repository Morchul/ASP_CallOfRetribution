using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    [SerializeField]
    private int port;

    [SerializeField]
    private string hostIP;

    [Header("Host")]
    [SerializeField]
    private Host hostPrefab;

    [Header("Client")]
    [SerializeField]
    private Client clientPrefab;

    public MessageTransmitter Transmitter { get; private set; }
    public IConnectionHandler ConnectionHandler { get; private set; }


    #region Singleton
    private static NetworkManager instance;

    public static NetworkManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Singleton %ScriptName% does already exist!");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    #endregion

    public void CreateHost()
    {
        ConnectionHandler = Instantiate(hostPrefab, this.transform);
        Transmitter = ConnectionHandler.GetTransmitter();
        ConnectionHandler.StartHandler(hostIP, port);
    }

    public void CreateClient(string hostIP)
    {
        ConnectionHandler = Instantiate(clientPrefab, this.transform);
        Transmitter = ConnectionHandler.GetTransmitter();
        ConnectionHandler.StartHandler(hostIP, port);
    }

    public void Shutdown()
    {
        if (ConnectionHandler != null)
            ConnectionHandler.Shutdown();
    }

    private void OnApplicationQuit()
    {
        Shutdown();
    }

    /*[SerializeField]
    private bool host;
    [SerializeField]
    private int delay;
    private void Start()
    {
        if(host)
            CreateHost();
        else
        {
            StartCoroutine(DelayedCreateClient());
        }
    }

    public void Test()
    {
        CreateClient();
    }

    private IEnumerator DelayedCreateClient()
    {
        yield return new WaitForSeconds(delay);
        CreateClient();
    }*/

}
