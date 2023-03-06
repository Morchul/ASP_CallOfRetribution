using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventController", menuName = "Controller/EventController")]
public class EventController : ScriptableObject
{
    /// <summary>
    /// Current events which wont be reseted:
    /// 
    /// </summary>

    [SerializeField]
    private GameEvent OnResetGame;

    [SerializeField]
    private GameEvent[] gameEventsToResetAfterGame;
    [SerializeField]
    private BoolEvent[] boolEventsToResetAfterGame;
    [SerializeField]
    private FloatEvent[] floatEventsToResetAfterGame;
    [SerializeField]
    private IntEvent[] intEventsToResetAfterGame;
    [SerializeField]
    private StringEvent[] stringEventsToResetAfterGame;
    [SerializeField]
    private Vector2Event[] vector2EventsToResetAfterGame;
    [SerializeField]
    private BugUpdateEvent[] bugUpdateEventsToResetAfterGame;

    public void Init()
    {
        OnResetGame.AddListener(ResetEvents);
    }

    private void ResetEvents()
    {
        foreach (GameEvent ge in gameEventsToResetAfterGame) ge.Reset();
        foreach (BoolEvent ge in boolEventsToResetAfterGame) ge.Reset();
        foreach (FloatEvent ge in floatEventsToResetAfterGame) ge.Reset();
        foreach (IntEvent ge in intEventsToResetAfterGame) ge.Reset();
        foreach (StringEvent ge in stringEventsToResetAfterGame) ge.Reset();
        foreach (Vector2Event ge in vector2EventsToResetAfterGame) ge.Reset();
        foreach (BugUpdateEvent ge in bugUpdateEventsToResetAfterGame) ge.Reset();
    }
}
