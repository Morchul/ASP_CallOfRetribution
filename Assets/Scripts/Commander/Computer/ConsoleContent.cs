using UnityEngine;

public class ConsoleContent
{
    private IConsoleElementData[] entries;

    private int counter;

    public ConsoleContent(int amount)
    {
        entries = new IConsoleElementData[amount];
        counter = 0;
    }

    public void Show(Console console)
    {
        console.Clear();
        for(int i = 0; i < counter; ++i)
        {
            console.DisplayEntry(entries[i]);
        }
        console.SetSelected(0);
    }

    public void AddEntry(IConsoleElementData data)
    {
        if(++counter > entries.Length)
        {
            Debug.LogError("ConsoleContent was created to small");
            return;
        }
        entries[counter - 1] = data;
    }
}
