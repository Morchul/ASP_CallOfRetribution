using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    private Selectable[] navigation;
    private int selected;
    private int navigationLength;

    [SerializeField]
    private TMP_Text consoleLog;

    [SerializeField]
    private TMP_InputField commandInput;

    public void SetNavigation(Selectable[] navigation)
    {
        this.navigation = navigation;
        selected = navigation.Length - 1;
        navigationLength = navigation.Length + 1;
        //SelectNext();
    }

    public void SubmitCommand()
    {
        if(selected == navigation.Length)
        {
            ExecuteCommand(commandInput.text);
        }
    }

    private void ExecuteCommand(string command)
    {
        commandInput.text = "";
        commandInput.ActivateInputField();
        AddLog("Execute command: " + command);
    }

    public void AddLog(string log)
    {
        string text = consoleLog.text;
        
        if(consoleLog.textInfo.lineCount > 10)
        {
            text = text.Substring(text.IndexOf('\n') + 1);
        }

        consoleLog.text = text + "\n" + log;
    }

    public void Enable()
    {
        Select(selected);
    }

    private void Select(int index)
    {
        if (index == navigation.Length) commandInput.Select();
        else
        {
            navigation[index].Select();
        }
        
        selected = index;
    }

    private bool IsSelectable(int index)
    {
        if (index == navigation.Length) return true;
        else return navigation[index].interactable;
    }

    public void SelectNext()
    {
        for (int i = 0; i < navigationLength; ++i)
        {
            int index = (selected + i + 1) % navigationLength;
            if (IsSelectable(index))
            {
                Select(index);
                break;
            }
        }
    }

    public void SelectPrevious()
    {
        for (int i = 0; i < navigationLength; ++i)
        {
            int index = (selected - i - 1);
            if (index < 0) index += navigationLength;
            if (IsSelectable(index))
            {
                Select(index);
                break;
            }
        }
    }
}
