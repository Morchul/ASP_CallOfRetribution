using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConversationHandler : MonoBehaviour, IPointerClickHandler
{
    [Header("Controller")]
    [SerializeField]
    private MissionController missionController;

    [Header("Events")]
    [SerializeField]
    private GameEvent OnGameReady;

    [Header("UI")]
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private Image rightImage;
    [SerializeField]
    private Image leftImage;

    private Conversation currentConversation;

    private void Awake()
    {
        OnGameReady.AddListener(StartConversation);
        gameObject.SetActive(false);
    }

    public void StartConversation()
    {
        StartConversation(missionController.CurrentMission.BeginConversation);
    }

    public void StartConversation(Conversation conversation)
    {
        if (conversation == null) return;

        gameObject.SetActive(true);
        currentConversation = conversation;
        currentConversation.StartConversation();
    }

    private void NextDialog()
    {
        if (currentConversation == null) return;

        if (currentConversation.IsConversationFinished) FinishConversation();
        else
        {
            Dialog dialog = currentConversation.GetNextDialog();
            text.text = dialog.Text;
            rightImage.sprite = dialog.rightImage;
            leftImage.sprite = dialog.leftImage;
        }
    }
    
    private void FinishConversation()
    {
        currentConversation = null;
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        NextDialog();
    }
}
