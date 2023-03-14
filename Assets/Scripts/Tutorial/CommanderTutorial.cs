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
    private CommanderTutorialStep computerTutorial;
    [SerializeField]
    private CommanderTutorialStep droneMoveTutorial;
    [SerializeField]
    private CommanderTutorialStep droneScanTutorial;
    [SerializeField]
    private CommanderTutorialStep droneFireFlareTutorial;
    [SerializeField]
    private CommanderTutorialStep bugTutorial;
    [SerializeField]
    private CommanderTutorialStep hackerTutorial;

    [Header("UI")]
    private TutorialTextBox currentTextBox;

    [Header("Events")]
    [SerializeField]
    private BoolEvent OnMissionFinished;
    [SerializeField]
    private GameEvent OnMissionLoaded;
    [SerializeField]
    private Vector2Event OnDroneMove;
    [SerializeField]
    private PosUpdateEvent OnPosUpdate;
    [SerializeField]
    private GameEvent OnDroneScan;
    [SerializeField]
    private Vector2Event OnGuardScanned;
    [SerializeField]
    private GameEvent OnDroneFlare;


    [Header("Objects")]
    [SerializeField]
    private DocumentHolder documentHolder;
    [SerializeField]
    private Map map;
    [SerializeField]
    private WorldObjectRefPos droneRefPosObj;
    [SerializeField]
    private Transform droneTutorialTargetPos;

    [Header("Controller")]
    [SerializeField]
    private MapData mapData;

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
        Computer,
        DroneMove,
        DroneScan,
        DroneFireFlare,
        Bug,
        Hacker,
        
    }

    private void Start()
    {
        if (NetworkManager.Instance.DEBUG_MODE)
        {
            OnMissionSelect.RaiseEvent(testMission.ID);

            stepCounter = TutorialSteps.None;
            OnMissionFinished.AddListener(FinishTutorial);
            OnDroneMove.AddListener(DroneMoveCommand);
            OnDroneScan.AddListener(DroneScanCommand);
            OnDroneFlare.AddListener(FlareMoveCommand);
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
            stepCounter == TutorialSteps.Computer
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

        if(stepCounter == TutorialSteps.DroneMove)
        {
            MoveDrone();
            if((droneTutorialTargetPos.position - droneRefPosObj.transform.position).sqrMagnitude < 0.25f)
            {
                NextStep();
            }
        }
    }

    #region MoveDrone
    private Vector3 targetDir;
    private Vector3 droneWorlPos;
    private float timer;

    private void MoveDrone()
    {
        if(targetDir != Vector3.zero)
        {
            if((timer += Time.deltaTime) >= 2) // 2 = Update time
            {
                droneWorlPos += (targetDir * 2 * Time.deltaTime); // 2 = move speed
                OnPosUpdate.RaiseEvent('D', droneWorlPos);
                timer = 0;
            }
        }
    }
    
    private void DroneMoveCommand(Vector2 mapCoordinatePos)
    {
        timer = 0;
        droneWorlPos = mapData.MapCoordinateToWorldPos(mapCoordinatePos);
        targetDir = (mapData.MapCoordinateToMapPos(mapCoordinatePos) - droneRefPosObj.transform.position.ToVector2()).normalized.ToVector3();
    }
    #endregion

    private void DroneScanCommand()
    {
        if(stepCounter == TutorialSteps.DroneScan)
        {
            OnGuardScanned.RaiseEvent(mapData.MapCoordinateToWorldPos(new Vector2(droneTutorialTargetPos.position.x + 1, droneTutorialTargetPos.position.y + 1.2f)).ToVector2());
            OnGuardScanned.RaiseEvent(mapData.MapCoordinateToWorldPos(new Vector2(droneTutorialTargetPos.position.x - 0.2f, droneTutorialTargetPos.position.y + 1f)).ToVector2());
            NextStep();
        }
    }

    private void FlareMoveCommand()
    {
        if (stepCounter == TutorialSteps.DroneFireFlare)
            NextStep();
    }

    private void FinishTutorial(bool successful)
    {
        NextStep();
        OnMissionFinished.RemoveListener(FinishTutorial);
        OnDroneMove.RemoveListener(DroneMoveCommand);
        OnDroneScan.RemoveListener(DroneScanCommand);
        OnDroneFlare.RemoveListener(FlareMoveCommand);
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
            case TutorialSteps.GameGoal: SetText(gameGoalTutorial); break;
            case TutorialSteps.Computer: SetText(computerTutorial); break;
            case TutorialSteps.DroneMove:
                droneTutorialTargetPos.gameObject.SetActive(true);
                SetText(droneMoveTutorial); 
                break;
            case TutorialSteps.DroneScan:
                droneTutorialTargetPos.gameObject.SetActive(false);
                SetText(droneScanTutorial);
                break;
            case TutorialSteps.DroneFireFlare: SetText(droneFireFlareTutorial); break;
            
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
