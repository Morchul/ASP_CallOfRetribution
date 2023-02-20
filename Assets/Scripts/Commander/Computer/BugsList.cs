using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugsList : Program
{

    [SerializeField]
    private ComputerButton[] bugReferences;

    [SerializeField]
    private MissionController missionController;

    public void Init()
    {
        BugReferenc[] bugs = missionController.Bugs;

        for (int i = 0; i < bugs.Length; ++i)
        {
            int constI = i;
            bugs[i].OnStatusChanged += bugReferences[i].UpdateUI;
            bugs[i].OnTypeChanged += bugReferences[i].UpdateUI;
            bugReferences[i].SetData(bugs[i]);
            bugReferences[i].OnSelect = () => SelectBug(bugs[constI]);
            AddNavigationItem(bugReferences[i].GetNavigationItem());
        }
    }

    public override void StartProgram()
    {
        base.StartProgram();
        foreach (ComputerButton bugRef in bugReferences)
            bugRef.UpdateUI();
    }

    private void SelectBug(BugReferenc bug)
    {
        computer.StartProgram(bug);
    }
}
