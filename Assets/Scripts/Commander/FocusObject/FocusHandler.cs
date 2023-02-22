using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusHandler : MonoBehaviour
{
    private IFocusable currentFocusObject;

    public bool BlockFocus { get; set; } = false;

    public void SetFocusObject(IFocusable focusable)
    {
        if (BlockFocus) return;

        if(currentFocusObject != null)
        {
            currentFocusObject.DisableFocus();
        }
        currentFocusObject = focusable;
        currentFocusObject.EnableFocus(new FocusObject.FocusAnimationParam(transform.position, transform.localEulerAngles));
    }

    public void StopFocus()
    {
        if (currentFocusObject != null)
        {
            currentFocusObject.DisableFocus();
        }
        currentFocusObject = null;
    }
}
