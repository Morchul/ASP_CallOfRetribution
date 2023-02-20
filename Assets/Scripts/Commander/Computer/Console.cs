using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    private Selectable[] navigation;
    private int selected;

    [SerializeField]
    private TMP_Text consoleLog;

    public void SetNavigation(Selectable[] navigation)
    {
        this.navigation = navigation;
        selected = navigation.Length - 1;
        //SelectNext();
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
        navigation[index].Select();
        selected = index;
    }

    public void SelectNext()
    {
        for (int i = 0; i < navigation.Length; ++i)
        {
            int index = (selected + i + 1) % navigation.Length;
            if (navigation[index].interactable)
            {
                Select(index);
                break;
            }
        }
    }

    public void SelectPrevious()
    {
        for (int i = 0; i < navigation.Length; ++i)
        {
            int index = (selected - i - 1);
            if (index < 0) index += navigation.Length;
            if (navigation[index].interactable)
            {
                Select(index);
                break;
            }
        }
    }
}
