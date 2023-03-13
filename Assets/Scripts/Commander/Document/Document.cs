using UnityEngine;
using TMPro;

public class Document : MovableFocusObject, IData
{
    //private Information info;
    [Header("Doc info")]
    [SerializeField]
    private int id;
    public int ID => id;

    [SerializeField]
    private bool startInfo;
    public bool StartInfo => startInfo;
}
