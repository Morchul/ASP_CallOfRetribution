using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField]
    private AudioClip[] musicTracks;

    [SerializeField]
    private AudioClip[] radioMessages;

    [SerializeField]
    private AudioClip radioTurnOnSound;

    [SerializeField]
    private AudioClip radioTurnOffSound;

    [Header("Settings")]
    [SerializeField]
    private int radioMessagesInQueue;

    [SerializeField]
    [Range(0, 100)]
    private int changeToPlayMusic;

    private LinkedList<AudioClip> radioSequence;

    private AudioSource audioSource;

    public bool IsRunning { get; private set; }
    private bool forceNextTrack;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        radioSequence = new LinkedList<AudioClip>();
        //FillRadioSequence(); DEBUG
        forceNextTrack = false;
    }

    void Update()
    {
        return; //DEBUG
        if(forceNextTrack)
        {
            //add effect like fade out
            audioSource.Stop();
        }

        if (!audioSource.isPlaying)
        {
            StartNextRadioClip();
        }
    }

    private void StartNextRadioClip()
    {
        forceNextTrack = false;
        radioSequence.RemoveFirst();
        audioSource.clip = radioSequence.First.Value;
        audioSource.Play();
        FillRadioSequence();
    }

    private void FillRadioSequence()
    {
        while (radioSequence.Count < radioMessagesInQueue)
            radioSequence.AddLast(GetNextRadioClip());
    }

    private AudioClip GetNextRadioClip()
    {
        if(Random.Range(0, 100) < changeToPlayMusic)
        {
            return musicTracks[Random.Range(0, musicTracks.Length)];
        }
        else
        {
            return radioMessages[Random.Range(0, radioMessages.Length)];
        }
    }

    public void TurnOnOff()
    {
        Debug.Log("TurnRadioOnOff: " + IsRunning);
        return; //DEBUG
        if (IsRunning)
            TurnOff();
        else
            TurnOn();
    }

    public void QueueMessage(RadioMessage message)
    {
        if (message.Important)
        {
            radioSequence.AddAfter(radioSequence.First, message.Clip);
            forceNextTrack = true;
        }
        else
        {
            radioSequence.AddLast(message.Clip);
        }
    }

    private void TurnOff()
    {
        audioSource.PlayOneShot(radioTurnOffSound);
        IsRunning = false;
        audioSource.mute = true;
    }

    private void TurnOn()
    {
        audioSource.PlayOneShot(radioTurnOnSound);
        IsRunning = true;
        audioSource.mute = false;
    }

    [System.Serializable]
    public class RadioMessage
    {
        public AudioClip Clip;
        public bool Important;
    }
}
