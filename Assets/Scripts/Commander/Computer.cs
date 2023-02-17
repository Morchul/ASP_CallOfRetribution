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
    #endregion

    [Header("Events")]
    [SerializeField]
    private BugPlacedEvent OnBugPlaced;

    private IBugable.Type[] bugReferenc;

    private void Awake()
    {
        OnBugPlaced.AddListener(BugPlaced);
        
    }

    public void SetAmountOfBugs(int amount)
    {
        bugReferenc = new IBugable.Type[amount];
        for(int i = 0; i < amount; ++i)
        {
            bugReferenc[i] = IBugable.Type.None;
        }
    }

    private void BugPlaced(int bugID, IBugable.Type type)
    {
        bugReferenc[bugID] = type;
    }

    #region Focus Handling
    public void EnableFocus(FocusAnimationParam focusPos)
    {
        focusHandler.BlockFocus = true;
        cameraHandler.FocusObject(cameraFocusPosition);
    }

    public void DisableFocus()
    {
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
