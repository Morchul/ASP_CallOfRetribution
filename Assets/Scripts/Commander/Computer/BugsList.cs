using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BugsList : Program
{
    [Header("Events")]
    [SerializeField]
    private BugPlacedEvent OnBugPlaced;

    [SerializeField]
    private BugReferencUI[] referencUI;

    [SerializeField]
    private int amountOfBugs = 3;

    [SerializeField]
    private Computer computer;

    private BugReferenc[] bugs;

    private void Awake()
    {
        OnBugPlaced.AddListener(BugPlaced);
        bugs = new BugReferenc[amountOfBugs];

        for (int i = 0; i < amountOfBugs; ++i)
        {
            bugs[i] = new BugReferenc(i);
            bugs[i].OnSelect = SelectBug;
            referencUI[i].SetData(bugs[i]);
        }
    }

    public override void StartProgram()
    {
        base.StartProgram();
        foreach (BugReferencUI bugUI in referencUI)
            bugUI.UpdateUI();
        referencUI[0].Focus();
    }

    private void SelectBug(BugReferenc bug)
    {
        computer.StartProgram(bug);
    }

    private void BugPlaced(int id, IBugable.Type type, int status)
    {
        bugs[id].Type = type;
        bugs[id].Status = status;
        referencUI[id].UpdateUI();
    }
}
