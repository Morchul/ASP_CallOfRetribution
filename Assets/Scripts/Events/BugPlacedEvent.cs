using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BugPlacedEvent", menuName = "Events/BugPlacedEvent")]
public class BugPlacedEvent : MonoBehaviour
{
	private event BugPlacedEventMethod _event;

	public delegate void BugPlacedEventMethod(int bugID, IBugable.Type type);

	public void AddListener(BugPlacedEventMethod listener)
	{
		_event += listener;
	}
	public void RemoveListener(BugPlacedEventMethod listener)
	{
		_event -= listener;
	}

	public void RaiseEvent(int bugID, IBugable.Type type)
	{
		_event?.Invoke(bugID, type);
	}
}
