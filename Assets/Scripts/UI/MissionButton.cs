using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionButton : MonoBehaviour
{
    [SerializeField]
    private Mission mission;

    [SerializeField]
    private Lobby lobby;
    [SerializeField]
    private ProgressController progressController;

    void Awake()
    {
        Button button = GetComponent<Button>();
        if (mission != null && progressController.GetCurrentProgress() >= mission.ID - 1)
        {
            button.onClick.AddListener(() => lobby.SelectMission(mission));
            button.interactable = true;
        }
        else
            button.interactable = false;
    }
}
