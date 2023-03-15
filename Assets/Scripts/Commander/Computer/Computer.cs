using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FocusObject;

public class Computer : MonoBehaviour, IFocusable
{
    #region Focus Handling
    [Header("Focus handling")]
    [SerializeField]
    private FocusHandler focusHandler;

    [SerializeField]
    private CommanderCameraController cameraHandler;

    [SerializeField]
    private Transform cameraFocusPosition;

    public bool InFocus { get; private set; }
    #endregion

    [SerializeField]
    private Console console;
    public Console Console => console;

    [SerializeField]
    private BugsList bugList;

    [SerializeField]
    private LockProgram lockProgram;

    [SerializeField]
    private AlarmProgram alarmProgram;

    private Program currentProgram;

    public bool Busy { get; set; }

    private void Start()
    {
        bugList.Init();
        ReturnToBugList();
    }

    public void ReturnToBugList()
    {
        StartProgram(bugList);
    }

    public void StartProgram(Program program)
    {
        if (currentProgram != null)
            currentProgram.CloseProgram();

        currentProgram = program;

        currentProgram.StartProgram();
        console.SetNavigation(currentProgram.GetNavigation());
    }

    public void CloseProgram()
    {
        currentProgram = null;
    }

    private void Update()
    {
        if(InFocus)
        {
            //Override the unity UI navigation
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                console.SelectPrevious();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Tab))
            {
                console.SelectNext();
            }
            else if(Input.GetKeyDown(KeyCode.Return))
            {
                console.SubmitCommand();
            }
        }
    }

    public void StartProgram(BugReferenc bugReferenc)
    {
        if(InFocus)
        {
            Debug.Log("Start program: " + bugReferenc.ID);
            switch (bugReferenc.Type)
            {
                case IBugable.Type.Lock:
                    lockProgram.SetBugReferenc(bugReferenc);
                    StartProgram(lockProgram); break;
                case IBugable.Type.Alarm:
                    alarmProgram.SetBugReferenc(bugReferenc);
                    StartProgram(alarmProgram); break;
                default: break;
            }
        }
    }

    #region Focus Handling
    public void EnableFocus(FocusAnimationParam focusPos)
    {
        InFocus = true;
        focusHandler.BlockFocus = true;
        console.Enable();
        cameraHandler.FocusObject(cameraFocusPosition);
    }

    public void DisableFocus()
    {
        InFocus = false;
        cameraHandler.StopFocus(AnimationFinished);
    }

    protected void AnimationFinished()
    {
        focusHandler.BlockFocus = false;
    }

    public virtual void OnMouseUp()
    {
        focusHandler.SetFocusObject(this);
    }
    #endregion
}
