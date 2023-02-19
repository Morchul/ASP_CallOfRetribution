using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugReferenc : IConsoleElementData
{
    public readonly int ID;

    public int Status;
    public IBugable.Type Type;

    public BugReferenc(int id)
    {
        ID = id;
    }

    public System.Action<BugReferenc> OnSelect;

    public string Name => "Bug " + ID + " type: " + Type;

    public bool Enabled => Type != IBugable.Type.None;

    public void Select() => OnSelect.Invoke(this);
}
