using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleEntry : BaseConsoleElement
{
    [SerializeField]
    private TMP_Text label;
    [SerializeField]
    private Image image;

    private bool focus;

    public override void SetFocus(bool focus)
    {
        this.focus = focus;
        SetColor();
    }

    public override void UpdateEntry()
    {
        base.UpdateEntry();

        label.text = data.Name;
        SetColor();
    }

    private void SetColor()
    {
        if (Enabled)
            image.color = (focus) ? Color.white : Color.black;
        else
            image.color = Color.grey;
    }
}
