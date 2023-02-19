using UnityEngine;

public class Console : MonoBehaviour
{
    [SerializeField]
    private BaseConsoleElement[] consoleElements;

    private int counter;
    private int selected;

    private void Awake()
    {
        counter = consoleElements.Length;
    }

    public void Show(ConsoleContent content)
    {
        content.Show(this);
    }

    public void UpdateDisplay()
    {
        for(int i = 0; i < counter; ++i)
        {
            consoleElements[i].UpdateEntry();
        }
    }

    public void Clear()
    {
        for (int i = 0; i < counter; ++i)
        {
            consoleElements[i].SetActive(false);
        }
        counter = 0;
    }

    public void DisplayEntry(IConsoleElementData data)
    {
        consoleElements[counter].SetActive(true);
        consoleElements[counter].SetData(data);
        ++counter;
        //return consoleEntries[counter];
    }

    public void SetSelected(int i)
    {
        if (selected > -1)
            consoleElements[selected].SetFocus(false);
        selected = i;
        consoleElements[selected].SetFocus(true);
    }

    public void SelectNext()
    {
        for(int i = 0; i < counter - 1; ++i)
        {
            int index = (selected + i + 1) % counter;
            if (consoleElements[index].Enabled)
            {
                SetSelected(index);
                break;
            }
        }
    }

    public void SelectPrevious()
    {
        for (int i = 0; i < counter - 1; ++i)
        {
            int index = (selected - i - 1);
            if (index < 0) index += counter;
            if (consoleElements[index].Enabled)
            {
                SetSelected(index);
                break;
            }
        }
    }

    public void SelectAction()
    {
        consoleElements[selected].Select();
    }
}
