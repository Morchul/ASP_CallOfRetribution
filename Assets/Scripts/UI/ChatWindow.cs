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
    //[SerializeField]
    //private StringEvent OnChatMessageSend;


    private void Start()
    {
        OnChatMessage.AddListener(NewChatMessage);
        chatInputField.onEndEdit.AddListener(SendNewMessage);

    }

    private void SendNewMessage(string message)
    {
        OnChatMessage.RaiseEvent(message);
    }

    private void NewChatMessage(string message)
    {
        chatEntries.text = message + "\n" + chatEntries.text;
    }
}
