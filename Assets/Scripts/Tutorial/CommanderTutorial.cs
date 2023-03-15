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
    private CommanderTutorialStep computerHandlingTutorial;
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
    [SerializeField]
    private CommanderTutorialStep finishTutorial;

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
    [SerializeField]
    private BugUpdateEvent OnBugUpdateRequest;
    [SerializeField]
    private BugUpdateEvent OnBugUpdate;
    [SerializeField]
    private IntEvent OnBugDenied;


    [Header("Objects")]
    [SerializeField]
    private DocumentHolder documentHolder;
    [SerializeField]
    private Map map;
    [SerializeField]
    private Computer computer;
    [SerializeField]
    private WorldObjectRefPos droneRefPosObj;
    [SerializeField]
    private Transform droneTutorialTarget;
    [SerializeField]
    private Hacker hacker;

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
        ComputerHandling,
        DroneMove,
        DroneScan,
        DroneFireFlare,
        Bug,
        Hacker,
        Finish,
        TutorialDone
        
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
            OnBugUpdateRequest.AddListener(BugUpdateRequest);
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
            stepCounter == TutorialSteps.ComputerHandling ||
            stepCounter == TutorialSteps.Finish
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
        if (stepCounter == TutorialSteps.Computer)
            if (computer.InFocus)
                NextStep();

        if(stepCounter == TutorialSteps.DroneMove)
        {
            MoveDrone();

            //Close enough to tutorial target, continue
            if((droneTutorialTarget.localPosition - droneRefPosObj.transform.localPosition).sqrMagnitude < 0.25f)
            {
                NextStep();
                OnPosUpdate.RaiseEvent('D', droneWorlPos);
            }
        }

        if(stepCounter == TutorialSteps.Hacker)
        {
            if(computer.Console.AttackedPort != 0)
            {
                //Attack defused
                hacker.Level = 0;
            }
            if (hacker.Level == 0 && computer.Console.AttackedPort == 0)
            {
                //Attack defused
                NextStep();
            }
        }
    }

    #region MoveDrone
    private Vector3 targetDir;
    private Vector3 droneWorlPos;
    private Vector3 droneTargetWorldPos;
    private float timer;

    private void MoveDrone()
    {
        if(targetDir != Vector3.zero)
        {
            droneWorlPos += (targetDir * 100 * Time.deltaTime); // 100 = move speed

            if ((timer += Time.deltaTime) >= 0.1f) // 0.1 = Update time
            {
                OnPosUpdate.RaiseEvent('D', droneWorlPos);
                timer = 0;
            }

            //target reached
            if ((droneWorlPos - droneTargetWorldPos).sqrMagnitude < 0.25f)
                targetDir = Vector3.zero;
        }
    }
    
    private void DroneMoveCommand(Vector2 mapCoordinatePos)
    {
        timer = 0;
        Vector2 localDroneMapPos = droneRefPosObj.transform.localPosition.ToVector2();
        droneWorlPos = mapData.MapPosToWorldPos(localDroneMapPos);
        droneTargetWorldPos = mapData.MapCoordinateToWorldPos(mapCoordinatePos);
        targetDir = (mapData.MapCoordinateToMapPos(mapCoordinatePos) - localDroneMapPos).normalized.ToVector3();
    }
    #endregion

    private void DroneScanCommand()
    {
        if(stepCounter == TutorialSteps.DroneScan)
        {
            OnGuardScanned.RaiseEvent(mapData.MapPosToWorldPos(droneTutorialTarget.localPosition.ToVector2() + new Vector2(0.4f, 0)).ToVector2());
            OnGuardScanned.RaiseEvent(mapData.MapPosToWorldPos(droneTutorialTarget.localPosition.ToVector2() + new Vector2(-0.2f, -0.1f)).ToVector2());
            NextStep();
        }
    }

    private void FlareMoveCommand()
    {
        if (stepCounter == TutorialSteps.DroneFireFlare)
            NextStep();
    }

    private void BugUpdateRequest(BugUpdateEvent.BugUpdate bugUpdate)
    {
        if(stepCounter == TutorialSteps.Bug)
        {
            if ((bugUpdate.Status & (int)ElectricalLock.LockState.Hacked) > 0) //If lock is hacked
            {
                hacker.Level = 0;
                OnBugUpdate.RaiseEvent(bugUpdate);

                if ((bugUpdate.Status & (int)ElectricalLock.LockState.Open) > 0)
                    NextStep();
            }
            else
            {
                OnBugDenied.RaiseEvent(0);
            }
        }
    }

    private void FinishTutorial(bool successful)
    {
        NextStep();
        OnMissionFinished.RemoveListener(FinishTutorial);
        OnDroneMove.RemoveListener(DroneMoveCommand);
        OnDroneScan.RemoveListener(DroneScanCommand);
        OnDroneFlare.RemoveListener(FlareMoveCommand);
        OnBugUpdateRequest.RemoveListener(BugUpdateRequest);
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
            case TutorialSteps.ComputerHandling: SetText(computerHandlingTutorial); break;
            case TutorialSteps.DroneMove:
                droneTutorialTarget.gameObject.SetActive(true);
                SetText(droneMoveTutorial); 
                break;
            case TutorialSteps.DroneScan:
                droneTutorialTarget.gameObject.SetActive(false);
                SetText(droneScanTutorial);
                break;
            case TutorialSteps.DroneFireFlare: SetText(droneFireFlareTutorial); break;
            case TutorialSteps.Bug:
                OnBugUpdate.RaiseEvent(0, IBugable.Type.Lock, 0);
                SetText(bugTutorial); break;
            case TutorialSteps.Hacker:
                hacker.Level = 1;
                SetText(hackerTutorial);
                break;
            case TutorialSteps.Finish:
                SetText(finishTutorial);
                break;
            case TutorialSteps.TutorialDone:
                OnMissionFinished.RaiseEvent(true);
                break;


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
