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

    [SerializeField]
    private BugsList bugList;

    [SerializeField]
    private LockProgram lockProgram;

    private void Awake()
    {
        bugList.StartProgram();
    }

    public void StartProgram(BugReferenc bugReferenc)
    {
        Debug.Log("Start program: " + bugReferenc.ID);
    }

    #region Focus Handling
    public void EnableFocus(FocusAnimationParam focusPos)
    {
        inFocus = true;
        console.Enable();
        focusHandler.BlockFocus = true;
        cameraHandler.FocusObject(cameraFocusPosition);
    }

    public void DisableFocus()
    {
        inFocus = false;
        console.Disable();
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
