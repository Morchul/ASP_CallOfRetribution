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

    private bool inFocus;
    #endregion

    [SerializeField]
    private Console console;

    [Header("Events")]
    [SerializeField]
    private BugPlacedEvent OnBugPlaced;

    private BugReferenc[] bugs;

    private ConsoleContent bugConsoleList;

    private void Awake()
    {
        OnBugPlaced.AddListener(BugPlaced);
        SetAmountOfBugs(3);
        console.Show(bugConsoleList);
    }

    public void SetAmountOfBugs(int amount)
    {
        bugs = new BugReferenc[amount];
        bugConsoleList = new ConsoleContent(amount);

        for (int i = 0; i < amount; ++i)
        {
            bugs[i] = new BugReferenc(i);
            bugs[i].OnSelect = StartProgram;
            bugConsoleList.AddEntry(bugs[i]);
        }
    }

    private void StartProgram(BugReferenc bugReferenc)
    {
        Debug.Log("Start program: " + bugReferenc.ID);
    }

    private void BugPlaced(int id, IBugable.Type type, int status)
    {
        bugs[id].Type = type;
        bugs[id].Status = status;
        console.UpdateDisplay();
    }

    private void Update()
    {
        if (inFocus)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                console.SelectNext();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                console.SelectPrevious();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                console.SelectAction();
            }
        }
    }

    #region Focus Handling
    public void EnableFocus(FocusAnimationParam focusPos)
    {
        inFocus = true;
        focusHandler.BlockFocus = true;
        cameraHandler.FocusObject(cameraFocusPosition);
    }

    public void DisableFocus()
    {
        inFocus = false;
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
