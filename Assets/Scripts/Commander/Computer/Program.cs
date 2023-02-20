using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program : MonoBehaviour
{
    public virtual void StartProgram()
    {
        gameObject.SetActive(true);
    }

    public virtual void CloseProgram()
    {
        gameObject.SetActive(false);
    }
}
