using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameMenuHandler : MonoBehaviour
{
    [Header("UI elements")]
    [SerializeField]
    private GameObject inGameMenuUI;
    [SerializeField]
    private GameObject finishScreen;
    [SerializeField]
    private TMP_Text finishText;

    [Header("Events")]
    [SerializeField]
    private BoolEvent OnMissionFinished;

    private int state; //0 = inactive / 1 = active / 2 = finished

    private void Awake()
    {
        state = 1;
        ShowHide();
        OnMissionFinished.AddListener(MissionFinished);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowHide();
        }
    }

    public void MissionFinished(bool successful)
    {
        if (state == 0) ShowHide();
        state = 2;

        finishScreen.SetActive(true);
        if (successful)
        {
            finishText.text = "Mission finished successful";
        }
        else
        {
            finishText.text = "Mission failed";
        }
    }

    public void ShowHide()
    {
        if (state == 2) return;
        state = (state + 1) % 2;
        inGameMenuUI.SetActive(state == 1);

        if(state == 1)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
