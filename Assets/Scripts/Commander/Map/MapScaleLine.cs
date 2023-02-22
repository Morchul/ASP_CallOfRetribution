using TMPro;
using UnityEngine;

public class MapScaleLine : MonoBehaviour
{
    private TMP_Text label;

    private void Awake()
    {
        label = GetComponentInChildren<TMP_Text>();
    }

    public void SetText(string text)
    {
        label.text = text;
    }

    public void SetPos(Vector2 pos)
    {
        this.transform.position = pos;
    }

    public void SmallLine()
    {
        this.transform.localScale = new Vector3(0.8f, 0.8f, 1);
    }
}
