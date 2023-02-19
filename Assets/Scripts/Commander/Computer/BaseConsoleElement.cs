using UnityEngine;

public abstract class BaseConsoleElement : MonoBehaviour, IConsoleElement
{
    protected IConsoleElementData data;
    public bool Enabled { get; protected set; }

    public abstract void SetFocus(bool focus);

    public virtual void UpdateEntry()
    {
        Enabled = data.Enabled;
    }

    public virtual void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public virtual void SetData(IConsoleElementData data)
    {
        Debug.Log("Set data for " + this.name + ": " + data.Name);
        this.data = data;
        
        UpdateEntry();
    }

    public virtual void Select()
    {
        if (Enabled)
            data.Select();
    }
}
