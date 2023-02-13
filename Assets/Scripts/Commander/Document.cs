using UnityEngine;
using TMPro;

public class Document : MonoBehaviour
{
    private Information info;

    private TMP_Text text;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    public void SetInformation(Information information)
    {
        
        info = information;
        Debug.Log("Info: " + info);
        Debug.Log("text: " + text);
        text.text = info.Message;
    }
}
