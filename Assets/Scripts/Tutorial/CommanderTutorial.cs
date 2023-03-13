using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommanderTutorial : MonoBehaviour
{
    [Header("Tutorial texts")]
    [SerializeField]
    private CommanderTutorialStep introduction;
    [SerializeField]
    private CommanderTutorialStep overview;
    [SerializeField]
    private CommanderTutorialStep documents;
    [SerializeField]
    private CommanderTutorialStep map;
    [SerializeField]
    private CommanderTutorialStep radio;
    [SerializeField]
    private CommanderTutorialStep gameGoal;
    [SerializeField]
    private CommanderTutorialStep informationGather;

    [Header("UI")]
    private TutorialTextBox currentTextBox;

    [Header("Events")]
    [SerializeField]
    private BoolEvent OnMissionFinished;
    [SerializeField]
    private GameEvent OnMissionLoaded;


    [Header("Objects")]


    [Header("Controller")]

    [Header("Test mission")]
    [SerializeField]
    private Mission testMission;
    [SerializeField]
    private IntEvent OnMissionSelect;


    private TutorialSteps stepCounter;

    private enum TutorialSteps : int // HAS TO BE IN ORDER!
    {
        None = 0,
        Introduction = 1,
        Overview,
        Documents,
        Map,
        Radio,
        GameGoal,
        InformationGather,
        Computer,
        Bug,
        Hacker,
        Drone
    }

    private void Start()
    {
        if (NetworkManager.Instance.DEBUG_MODE)
        {
            OnMissionSelect.RaiseEvent(testMission.ID);

            stepCounter = TutorialSteps.None;
            OnMissionFinished.AddListener(FinishTutorial);
            NextStep();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    /*private void Start()
    {
        if (NetworkManager.Instance.DEBUG_MODE)
        {
           
        }
    }*/

    private void Update()
    {
        if (stepCounter == TutorialSteps.Introduction ||
            stepCounter == TutorialSteps.Overview ||
            stepCounter == TutorialSteps.Documents ||
            stepCounter == TutorialSteps.Map ||
            stepCounter == TutorialSteps.Radio
            )
        {
            if (Input.GetKeyDown(KeyCode.F))
                NextStep();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            currentTextBox.Hide();
        }
    }

    private void FinishTutorial(bool successful)
    {
        NextStep();
        OnMissionFinished.RemoveListener(FinishTutorial);
    }

    public void NextStep()
    {
        stepCounter = (TutorialSteps)((int)stepCounter + 1);

        if(currentTextBox != null)
            currentTextBox.SetText("");

        switch (stepCounter)
        {
            case TutorialSteps.Introduction: SetText(introduction); break;
            case TutorialSteps.Overview: SetText(overview); break;
            case TutorialSteps.Documents: SetText(documents); break;
            case TutorialSteps.Map: SetText(map); break;
            case TutorialSteps.Radio: SetText(radio); break;
            
            default:
                break;

        }
    }

    private void SetText(CommanderTutorialStep step)
    {
        currentTextBox = step.Box;
        currentTextBox.SetText(step.Text);
    }

    [System.Serializable]
    public struct CommanderTutorialStep
    {
        [TextArea(3,10)]
        public string Text;
        public TutorialTextBox Box;
    }
}
