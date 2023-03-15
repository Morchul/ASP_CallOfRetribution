using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFocusable
{
    public bool InFocus { get; }

    public void DisableFocus();
    public void EnableFocus(FocusObject.FocusAnimationParam focusPos);
}
