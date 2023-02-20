using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Program : MonoBehaviour
{
    [SerializeField]
    protected Computer computer;

    protected List<Selectable> navigation;

    protected virtual void Awake()
    {
        navigation = new List<Selectable>();
    }

    public virtual void StartProgram()
    {
        gameObject.SetActive(true);
    }

    public virtual void CloseProgram()
    {
        gameObject.SetActive(false);
        computer.CloseProgram();
    }

    public Selectable[] GetNavigation()
    {
        return navigation.ToArray();
    }

    protected void AddNavigationItem(Selectable selectable)
    {
        navigation.Add(selectable);
    }
}
