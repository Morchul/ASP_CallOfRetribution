using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BugReferencUI : MonoBehaviour
{
    private TMP_Text text;
    private Button button;
    private BugReferenc bugReferenc;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
        button = GetComponent<Button>();
        button.onClick.AddListener(() => bugReferenc.Select());
    }

    public void Focus()
    {
        button.Select();
    }

    public void SetData(BugReferenc bugReferenc)
    {
        this.bugReferenc = bugReferenc;
    }

    public void UpdateUI()
    {
        text.text = bugReferenc.Name;
        button.interactable = bugReferenc.Enabled;
    }
}
