using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugsList : Program
{

    [SerializeField]
    private ComputerButton[] bugReferences;

    [SerializeField]
    private MissionController missionController;

    private int activeBugRefernces;

    public void Init()
    {
        BugReferenc[] bugs = missionController.Bugs;
        activeBugRefernces = bugs.Length;

        for (int i = 0; i < bugs.Length; ++i)
        {
            int constI = i;
            bugReferences[i].gameObject.SetActive(true);
            bugs[i].OnTypeChanged += bugReferences[i].UpdateUI;
            bugReferences[i].SetData(bugs[i]);
            bugReferences[i].OnSelect = () => SelectBug(bugs[constI]);
            AddNavigationItem(bugReferences[i].GetNavigationItem());
        }

        for(int i = bugs.Length; i < bugReferences.Length; ++i)
        {
            bugReferences[i].gameObject.SetActive(false);
        }
    }

    public override void StartProgram()
    {
        base.StartProgram();
        for (int i = 0; i < activeBugRefernces; ++i)
            bugReferences[i].UpdateUI();
    }

    private void SelectBug(BugReferenc bug)
    {
        computer.StartProgram(bug);
    }
}
