using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionButton : MonoBehaviour
{
    [SerializeField]
    private Mission mission;

    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private ProgressController progressController;

    // Start is called before the first frame update
    void Awake()
    {
        Button button = GetComponent<Button>();
        if (mission != null && progressController.GetCurrentProgress() >= mission.ID - 1)
        {
            button.onClick.AddListener(() => gameController.SelectMission(mission));
            button.interactable = true;
        }
        else
            button.interactable = false;
    }
}
