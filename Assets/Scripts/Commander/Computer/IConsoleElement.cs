using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsoleElement
{
    public void SetFocus(bool focus);
    public void SetActive(bool active);
    public void SetData(IConsoleElementData data);
    public void UpdateEntry();
    public void Select();
}
