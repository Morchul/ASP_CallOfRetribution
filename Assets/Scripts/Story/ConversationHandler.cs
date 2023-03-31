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
    [SerializeField]
    private GameController gameController;

    [Header("Events")]
    [SerializeField]
    private GameEvent OnGameReady;
    [SerializeField]
    private BoolEvent OnMissionFinished;

    [Header("UI")]
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private Image rightImage;
    [SerializeField]
    private Image leftImage;

    private Conversation currentConversation;

    private AudioSource audioSource;

    private void Awake()
    {
        OnGameReady.AddListener(StartBeginConversation);
        OnMissionFinished.AddListener(StartEndConversation);
        gameObject.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    public void StartBeginConversation()
    {
        StartConversation(missionController.CurrentMission.BeginConversation);
    }

    private void StartEndConversation(bool successful)
    {
        if(successful)
            StartConversation(missionController.CurrentMission.EndConversation);
    }

    public void StartConversation(Conversation conversation)
    {
        if (conversation == null) return;

        gameController.DisableInput();

        gameObject.SetActive(true);
        currentConversation = conversation;
        currentConversation.StartConversation();
        NextDialog();
        audioSource.clip = currentConversation.BackgroundMusic;
        if(audioSource.clip != null)
            audioSource.Play();
    }

    public void SkipConversation()
    {
        FinishConversation();
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
        audioSource.Stop();
        gameObject.SetActive(false);
        gameController.EnableInput();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            NextDialog();
        else if (eventData.button == PointerEventData.InputButton.Right)
            SkipConversation();
    }
}
