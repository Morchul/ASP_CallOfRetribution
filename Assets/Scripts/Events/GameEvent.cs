using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Events/GameEvent")]
public class GameEvent : ScriptableObject
{
	private event GameEventMethod _event;

	public delegate void GameEventMethod();

	public void AddListener(GameEventMethod listener)
	{
		_event += listener;
	}
	public void RemoveListener(GameEventMethod listener)
	{
		_event -= listener;
	}

	public virtual void RaiseEvent()
	{
		_event?.Invoke();
	}

	public virtual void Reset()
	{
		_event = null;
	}
}

public class GameEvent<T> : ScriptableObject
{
	private event GameEventMethod _event;

	public delegate void GameEventMethod(T value);

	public void AddListener(GameEventMethod listener)
	{
		_event += listener;
	}
	public void RemoveListener(GameEventMethod listener)
	{
		_event -= listener;
	}

	public virtual void RaiseEvent(T value)
	{
		_event?.Invoke(value);
	}

	public virtual void Reset()
	{
		_event = null;
	}
}
