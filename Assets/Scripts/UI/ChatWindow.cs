using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatWindow : MonoBehaviour
{
    [Header("UI elements")]
    [SerializeField]
    private TextMeshProUGUI chatEntries;

    [SerializeField]
    private TMP_InputField chatInputField;

    [Header("Events")]
    [SerializeField]
    private StringEvent OnChatMessageReceived;


    void Start()
    {
        OnChatMessageReceived.AddListener(NewChatMessage);
        chatInputField.onEndEdit.AddListener(SendNewMessage);

    }

    private void SendNewMessage(string message)
    {
        NetworkManager.Instance.Transmitter.WriteToHost(MessageUtility.CreateChatMessage(message));
    }

    private void NewChatMessage(string message)
    {
        chatEntries.text = message + "\n" + chatEntries.text;
    }
}
