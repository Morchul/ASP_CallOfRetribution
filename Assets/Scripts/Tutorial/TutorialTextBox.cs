using TMPro;
using UnityEngine;

public class TutorialTextBox : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    public void SetText(string text)
    {
        gameObject.SetActive(text != "");
        this.text.text = text;
    }

    public void Hide()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
