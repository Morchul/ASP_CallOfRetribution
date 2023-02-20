using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ComputerButton : MonoBehaviour
{
    protected TMP_Text text;
    protected Button button;

    private IComputerButtonData buttonData;

    public System.Action OnSelect;

    protected virtual void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Select); //UnityAction => System.Action
    }

    public virtual void Select()
    {
        OnSelect();
    }

    public Selectable GetNavigationItem()
    {
        return button;
    }

    public virtual void SetData(IComputerButtonData buttonData)
    {
        this.buttonData = buttonData;
    }

    public virtual void UpdateUI()
    {
        text.text = buttonData.Name;
        button.interactable = buttonData.Enabled;
    }

    public interface IComputerButtonData
    {
        public string Name { get; }
        public bool Enabled { get; }
    }
}
