using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerCheckbox : ComputerButton
{
    private bool isChecked;

    private Image image;

    protected override void Awake()
    {
        base.Awake();
        isChecked = false;
        image = GetComponent<Image>();
    }

    public override void Select()
    {
        if (!isChecked)
        {
            base.Select();
            isChecked = !isChecked;
        }
    }

    public override void UpdateUI()
    {
        base.UpdateUI();
        //show checked
    }
}
