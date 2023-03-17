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
    private StringEvent OnChatMessage;

    private string chatName;

    private void Start()
    {
        OnChatMessage.AddListener(NewChatMessage);
        chatInputField.onEndEdit.AddListener(SendNewMessage);

        chatName = NetworkManager.Instance.ConnectionHandler.IsHost() ? "Thief: " : "Commander: ";
    }

    private void SendNewMessage(string message)
    {
        OnChatMessage.RaiseEvent(chatName + message);
    }

    private void NewChatMessage(string message)
    {
        chatEntries.text = message + "\n" + chatEntries.text;
    }
}
