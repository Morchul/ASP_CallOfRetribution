using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BugPlacedEvent", menuName = "Events/BugPlacedEvent")]
public class BugPlacedEvent : ScriptableObject
{
	private event BugPlacedEventMethod _event;

	public delegate void BugPlacedEventMethod(int ID, IBugable.Type type, int status);

	public void AddListener(BugPlacedEventMethod listener)
	{
		_event += listener;
	}
	public void RemoveListener(BugPlacedEventMethod listener)
	{
		_event -= listener;
	}

	public void RaiseEvent(int ID, IBugable.Type type, int status)
	{
		_event?.Invoke(ID, type, status);
	}
}
