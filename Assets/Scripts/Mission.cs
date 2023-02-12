using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "Mission/Mission")]
public class Mission : ScriptableObject
{
    [Scene]
    public string MissionScene;

    public int ID;

    [SerializeField]
    private Information[] missionInformation;

    public IntEvent OnNewInformation;

    public void Start()
    {
        foreach(Information info in missionInformation)
        {
            if (info.StartInfo)
            {
                OnNewInformation.RaiseEvent(info.ID);
            }
        }
    }

    public Information GetInformation(int id)
    {
        return missionInformation.First((info) => info.ID == id);
    }
}
