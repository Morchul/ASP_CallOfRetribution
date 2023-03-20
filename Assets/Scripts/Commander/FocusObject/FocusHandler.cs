using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusHandler : MonoBehaviour
{
    public IFocusable CurrentFocusObject { get; private set; }

    public bool BlockFocus { get; set; } = false;

    public void SetFocusObject(IFocusable focusable)
    {
        if (BlockFocus) return;

        if(CurrentFocusObject != null)
        {
            CurrentFocusObject.DisableFocus();
        }
        CurrentFocusObject = focusable;
        CurrentFocusObject.EnableFocus(new FocusObject.FocusAnimationParam(transform.position, transform.localEulerAngles));
    }

    public void StopFocus()
    {
        if (CurrentFocusObject != null)
        {
            CurrentFocusObject.DisableFocus();
        }
        CurrentFocusObject = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StopFocus();
        }
    }
}
