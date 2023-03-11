using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThiefTutorial : MonoBehaviour
{
    [Header("Tutorial texts")]
    [SerializeField]
    [TextArea(3, 10)]
    private string introductionText;

    [SerializeField]
    [TextArea(3, 10)]
    private string basicControlTutorial;

    [SerializeField]
    [TextArea(3, 10)]
    private string findDroneTutorial;

    [SerializeField]
    [TextArea(3, 10)]
    private string droneFeatureTutorial;

    [SerializeField]
    [TextArea(3, 10)]
    private string interactionTutorial;

    [SerializeField]
    [TextArea(3, 10)]
    private string placeBugTutorial;

    [SerializeField]
    [TextArea(3, 10)]
    private string bugFeatureTutorial;

    [SerializeField]
    [TextArea(3, 10)]
    private string gameGoalTutorial;

    [SerializeField]
    [TextArea(3, 10)]
    private string informationGatherTutorial;

    [SerializeField]
    [TextArea(3, 10)]
    private string stealItemTutorial;

    [SerializeField]
    [TextArea(3, 10)]
    private string alarmTutorial;

    [SerializeField]
    [TextArea(3, 10)]
    private string distruberTutorial;

    [SerializeField]
    [TextArea(3, 10)]
    private string guardsTutorial;

    [SerializeField]
    [TextArea(3, 10)]
    private string extractionPointTutorial;

    [SerializeField]
    [TextArea(3, 10)]
    private string finishThiefTutorial;

    [Header("UI")]
    [SerializeField]
    private TMP_Text tutorialText;

    [Header("Events")]
    [SerializeField]
    private GameEvent OnDroneFireFlare;
    [SerializeField]
    private IntEvent OnNewInformation;
    [SerializeField]
    private BugUpdateEvent OnBugUpdate;
    [SerializeField]
    private BugUpdateEvent OnBugUpdateRequest;
    [SerializeField]
    private GameEvent OnItemStolen;
    [SerializeField]
    private GameEvent OnMissionCompletedSuccessfully;

    [Header("Objects")]
    [SerializeField]
    private Drone drone;
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private Alarm alarm;
    [SerializeField]
    private ExtractionPoint extractionPoint;
    [SerializeField]
    private GameObject tutorialPart2; //interact
    [SerializeField]
    private GameObject tutorialPart3; //bug
    [SerializeField]
    private GameObject tutorialPart4; //final

    [Header("Controller")]
    [SerializeField]
    private GameController gameController;

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
        BasicControl,
        FindDrone,
        DroneFeature,
        Interaction,
        PlaceBug,
        BugFeature,
        GameGoal,
        InformationGather,
        StealItem,
        Alarm,
        Disturber,
        Guards,
        ExtractionPoint,
        Finish
    }

    private void Awake()
    {
        OnMissionSelect.RaiseEvent(testMission.ID);

        stepCounter = TutorialSteps.None;
        OnBugUpdate.AddListener(OnBugUpdateEvent);
        OnItemStolen.AddListener(OnItemStolenEvent);
        OnMissionCompletedSuccessfully.AddListener(FinishTutorial);
        OnNewInformation.AddListener(InformationGathered);
        NextStep();
    }
    private void Update()
    {
        if(stepCounter == TutorialSteps.Introduction ||
            stepCounter == TutorialSteps.BasicControl ||
            stepCounter == TutorialSteps.DroneFeature ||
            stepCounter == TutorialSteps.BugFeature ||
            stepCounter == TutorialSteps.GameGoal ||
            stepCounter == TutorialSteps.InformationGather ||
            stepCounter == TutorialSteps.Guards ||
            stepCounter == TutorialSteps.Finish
            )
        {
            if (Input.GetKeyDown(KeyCode.F))
                NextStep();
        }

        if(stepCounter == TutorialSteps.DroneFeature)
        {
            if (Input.GetKeyDown(KeyCode.R))
                OnDroneFireFlare.RaiseEvent();
        }

        if(stepCounter == TutorialSteps.FindDrone)
        {
            float dotProduct = Vector3.Dot(playerCamera.transform.forward, (drone.transform.position - playerCamera.transform.position).normalized);
            if (dotProduct > 0.97f)
            {
                NextStep();
            }
        }

        if (stepCounter == TutorialSteps.StealItem)
            if (alarm.Bugged)
                NextStep();

        if(stepCounter == TutorialSteps.ExtractionPoint)
        {
            if((timer += Time.deltaTime) > 5)
            {
                OnDroneFireFlare.RaiseEvent();
                timer = 0;
            }
        }
    }

    private int stateHelper;
    private float timer;
    public void EnterTrigger(int triggerID)
    {
        //If document is picked up and enter trigger behind door
        if(triggerID == 1 && stateHelper == 1 && stepCounter == TutorialSteps.Interaction)
        {
            NextStep();
        }
        if(triggerID == 2 && stepCounter == TutorialSteps.BugFeature)
        {
            NextStep();
        }
    }

    private void OnBugUpdateEvent(int bugID, IBugable.Type type, int status)
    {
        if ((stepCounter == TutorialSteps.PlaceBug && type == IBugable.Type.Lock) ||
            (stepCounter == TutorialSteps.Alarm && type == IBugable.Type.Alarm))
        {
            stateHelper = bugID;
            NextStep();
        }
    }

    private void OnItemStolenEvent()
    {
        NextStep();
    }

    private void FinishTutorial()
    {
        NextStep();
        OnBugUpdate.RemoveListener(OnBugUpdateEvent);
        OnItemStolen.RemoveListener(OnItemStolenEvent);
        OnMissionCompletedSuccessfully.RemoveListener(FinishTutorial);
        OnNewInformation.RemoveListener(InformationGathered);
    }

    private void InformationGathered(int _)
    {
        stateHelper = 1;
    }

    public void NextStep()
    {
        stepCounter = (TutorialSteps)((int)stepCounter + 1);
        switch (stepCounter)
        {
            case TutorialSteps.Introduction: tutorialText.text = introductionText; break;
            case TutorialSteps.BasicControl: tutorialText.text = basicControlTutorial; break;
            case TutorialSteps.FindDrone: tutorialText.text = findDroneTutorial;break;
            case TutorialSteps.DroneFeature: tutorialText.text = droneFeatureTutorial;break;
            case TutorialSteps.Interaction:
                tutorialText.text = interactionTutorial;
                tutorialPart2.SetActive(true);
                stateHelper = 0;
                break;
            case TutorialSteps.PlaceBug:
                tutorialText.text = placeBugTutorial;
                tutorialPart3.SetActive(true);
                stateHelper = 0;
                break;
            case TutorialSteps.BugFeature:
                tutorialText.text = bugFeatureTutorial;
                OnBugUpdateRequest.RaiseEvent(stateHelper, IBugable.Type.Lock, (int)ElectricalLock.LockState.Open | (int)ElectricalLock.LockState.Hacked);
                break;
            case TutorialSteps.GameGoal:
                tutorialText.text = gameGoalTutorial;
                break;
            case TutorialSteps.InformationGather:
                tutorialText.text = informationGatherTutorial;
                break;
            case TutorialSteps.StealItem:
                tutorialText.text = stealItemTutorial;
                tutorialPart4.SetActive(true);
                drone.MoveCommand(extractionPoint.transform.position);
                break;
            case TutorialSteps.Alarm:
                tutorialText.text = alarmTutorial;
                stateHelper = 0;
                break;
            case TutorialSteps.Disturber:
                tutorialText.text = distruberTutorial;
                OnBugUpdateRequest.RaiseEvent(stateHelper, IBugable.Type.Alarm, (int)Alarm.AlarmState.Disabled | (int)Alarm.AlarmState.Hacked);
                break;
            case TutorialSteps.Guards:
                extractionPoint.gameObject.SetActive(false);
                tutorialText.text = guardsTutorial;
                break;
            case TutorialSteps.ExtractionPoint:
                timer = 4.5f;
                extractionPoint.gameObject.SetActive(true);
                tutorialText.text = extractionPointTutorial;
                break;
            case TutorialSteps.Finish:
                tutorialText.text = finishThiefTutorial;
                break;
            default:
                break;

        }
    }
}
