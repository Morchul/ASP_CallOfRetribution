using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    private List<int> currentAvailableInformation;

    public IntEvent OnNewInformation;

    private void Start()
    {
        OnNewInformation.AddListener((int id) => currentAvailableInformation.Add(id));
    }

    public void StartMission(Mission mission)
    {

    }

    public void FinishMission()
    {
        currentAvailableInformation.Clear();
    }
}
