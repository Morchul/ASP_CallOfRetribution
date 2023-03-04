using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "Mission/Mission")]
public class Mission : ScriptableObject
{
    [Scene]
    public string MissionScene;

    public int ID;

    [SerializeField]
    private Document[] missionDocuments;

    [SerializeField]
    private RadioMessage[] radioMessages;

    [SerializeField]
    private int amountOfBugs;
    public int AmountOfBugs => amountOfBugs;

    [Header("Events")]
    public IntEvent OnNewInformation;

    [Header("Story")]
    public Conversation BeginConversation;
    public Conversation EndConversation;

    public void Start()
    {
        foreach(Document doc in missionDocuments)
        {
            if (doc.StartInfo)
            {
                OnNewInformation.RaiseEvent(doc.ID);
            }
        }
    }

    public RadioMessage GetRadioMessage(int id)
    {
        return radioMessages.First((message) => message.ID == id);
    }

    public Document GetInformation(int id)
    {
        return missionDocuments.First((info) => info.ID == id);
    }

    [System.Serializable]
    public class RadioMessage
    {
        public int ID;
        public AudioClip AudioClip;
        public bool Important;
    }
}
