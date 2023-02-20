using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;
        Disable();
    }

    public void Enable()
    {
        //canvasGroup.blocksRaycasts = true;
        //canvasGroup.interactable = true;
    }

    public void Disable()
    {
        //canvasGroup.blocksRaycasts = false;
        //canvasGroup.interactable = false;
        //EventSystem.current.SetSelectedGameObject(null);
    }
}
