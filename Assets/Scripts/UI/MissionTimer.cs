using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionTimer : MonoBehaviour
{
    [SerializeField]
    private BoolEvent OnMissionFinished;

    [SerializeField]
    private GameEvent OnGameReady;

    [SerializeField]
    private MissionController missionController;

    [SerializeField]
    private TMP_Text timerDisplay;

    private bool running;

    private float timeRemaining;

    private void Awake()
    {
        OnGameReady.AddListener(StartTimer);
        OnMissionFinished.AddListener(StopTimer);
        running = false;
    }

    private void StartTimer()
    {
        running = true;
        timeRemaining = missionController.CurrentMission.TimeInSeconds;
    }
    
    private void StopTimer(bool successful)
    {
        if (running && !successful)
        {
            UpdateUI(0, 0);
        }  
        running = false;
    }

    private void Update()
    {
        if(running)
        {
            timeRemaining -= Time.deltaTime;

            int minutes = (int)(timeRemaining / 60);
            int seconds = (int)(timeRemaining % 60);

            UpdateUI(minutes, seconds);

            if(timeRemaining <= 0)
            {
                OnMissionFinished.RaiseEvent(false);
            }
        }
    }

    private void UpdateUI(int minutes, int seconds)
    {
        timerDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
