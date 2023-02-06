using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StringEvent", menuName = "Events/StringEvent")]
public class StringEvent : ScriptableObject
{
	private event StringEventMethod _event;

	public delegate void StringEventMethod(string value);

	public void AddListener(StringEventMethod listener)
	{
		_event += listener;
	}
	public void RemoveListener(StringEventMethod listener)
	{
		_event -= listener;
	}

	public void RaiseEvent(string value)
	{
		_event?.Invoke(value);
	}
}
