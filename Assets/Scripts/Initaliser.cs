using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initaliser : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField]
    private MissionController missionController;

    void Awake()
    {
        missionController.Init();
    }
}
