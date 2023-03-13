using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommanderTutorial : MonoBehaviour
{
    [Header("Tutorial texts")]
    [SerializeField]
    private CommanderTutorialStep introductionTutorial;
    [SerializeField]
    private CommanderTutorialStep overviewTutorial;
    [SerializeField]
    private CommanderTutorialStep documentsTutorial;
    [SerializeField]
    private CommanderTutorialStep mapTutorial;
    [SerializeField]
    private CommanderTutorialStep radioTutorial;
    [SerializeField]
    private CommanderTutorialStep gameGoalTutorial;
    [SerializeField]
    private CommanderTutorialStep informationGatherTutorial;

    [Header("UI")]
    private TutorialTextBox currentTextBox;

    [Header("Events")]
    [SerializeField]
    private BoolEvent OnMissionFinished;
    [SerializeField]
    private GameEvent OnMissionLoaded;


    [Header("Objects")]
    [SerializeField]
    private DocumentHolder documentHolder;
    [SerializeField]
    private Map map;

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

    private void Update()
    {
        if (stepCounter == TutorialSteps.Introduction ||
            stepCounter == TutorialSteps.Radio ||
            stepCounter == TutorialSteps.GameGoal ||
            stepCounter == TutorialSteps.InformationGather
            )
        {
            if (Input.GetKeyDown(KeyCode.F))
                NextStep();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            currentTextBox.Hide();
        }

        if(stepCounter == TutorialSteps.Overview)
        {
            foreach(Document doc in documentHolder.Documents)
            {
                if (doc.InFocus)
                {
                    NextStep();
                    break;
                }
            }
        }

        if (stepCounter == TutorialSteps.Documents)
            if (map.InFocus)
                NextStep();
        if (stepCounter == TutorialSteps.Map)
            if (!map.InFocus)
                NextStep();

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
            case TutorialSteps.Introduction: SetText(introductionTutorial); break;
            case TutorialSteps.Overview: SetText(overviewTutorial); break;
            case TutorialSteps.Documents: SetText(documentsTutorial); break;
            case TutorialSteps.Map: SetText(mapTutorial); break;
            case TutorialSteps.Radio: SetText(radioTutorial); break;
            
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
