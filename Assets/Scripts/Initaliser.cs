using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initaliser : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField]
    private MissionController missionController;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private EventController eventController;

    void Awake()
    {
        gameController.Init();
        missionController.Init();
        eventController.Init();
    }
}
