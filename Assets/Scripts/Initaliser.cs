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

    private static bool initialised = false;

    void Awake()
    {
        gameController.Init(); //Events will be reset so call every time
        missionController.Init(); //Events will be reset so call every time

        if (!initialised)
        {
            eventController.Init(); //Events won't be reset, call only once

            DontDestroyOnLoad(this.gameObject);
            initialised = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
