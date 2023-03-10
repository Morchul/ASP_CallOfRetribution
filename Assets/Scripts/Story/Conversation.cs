using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "Story/Conversation")]
public class Conversation : ScriptableObject
{
    [SerializeField]
    private Dialog[] dialogs;

    private int counter;

    public void StartConversation()
    {
        counter = -1;
    }

    public bool IsConversationFinished => counter == dialogs.Length - 1;

    public Dialog GetNextDialog()
    {
        return dialogs[++counter];
    }
}
