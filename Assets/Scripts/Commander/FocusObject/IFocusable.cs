using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFocusable
{
    public void DisableFocus();
    public void EnableFocus(FocusObject.FocusAnimationParam focusPos);
}
