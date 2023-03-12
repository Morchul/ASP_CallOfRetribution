using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BugPlacedEvent", menuName = "Events/BugPlacedEvent")]
public class BugUpdateEvent : ScriptableObject
{
	private event BugUpdateEventMethod _event;

	public delegate void BugUpdateEventMethod(int ID, IBugable.Type type, int status);

	public void AddListener(BugUpdateEventMethod listener)
	{
		_event += listener;
	}
	public void RemoveListener(BugUpdateEventMethod listener)
	{
		_event -= listener;
	}

	public void RaiseEvent(int ID, IBugable.Type type, int status)
	{
		_event?.Invoke(ID, type, status);
	}

	public void Reset()
	{
		_event = null;
	}
}
