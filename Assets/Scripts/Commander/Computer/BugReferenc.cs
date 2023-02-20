using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugReferenc : ComputerButton.IComputerButtonData
{
    public readonly int ID;

    public int Status { get; private set; }
    public IBugable.Type Type { get; private set; }

    public event System.Action OnStatusChanged;
    public event System.Action OnTypeChanged;

    public BugReferenc(int id)
    {
        ID = id;
    }

    public void Update(IBugable.Type type, int status)
    {
        if(Type != type)
        {
            Type = type;
            OnTypeChanged?.Invoke();
        }

        if(Status != status)
        {
            Status = status;
            OnStatusChanged?.Invoke();
        }
    }

    #region IComputerButtonData
    public string Name => "Bug " + ID + " type: " + Type;
    public bool Enabled => Type != IBugable.Type.None;
    #endregion
}
