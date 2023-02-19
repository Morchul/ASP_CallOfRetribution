using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsoleElementData
{
    public string Name { get; }
    public bool Enabled { get; }
    public void Select();
}
