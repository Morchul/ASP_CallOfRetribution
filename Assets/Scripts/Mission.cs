using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "Mission/Mission")]
public class Mission : ScriptableObject
{
    [Scene]
    public string MissionScene;

    [SerializeField]
    private InformationData[] missionInformation;

    public IntEvent OnNewInformation;

    public void Start()
    {
        foreach(InformationData infoData in missionInformation)
        {
            if (infoData.StartInfo)
            {
                OnNewInformation.RaiseEvent(infoData.Info.ID);
            }
        }
    }

    public Information GetInformation(int id)
    {
        return missionInformation.First((info) => info.Info.ID == id).Info;
    }

    [System.Serializable]
    public struct InformationData
    {
        public Information Info;
        public bool StartInfo;
    }
}
