using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Events/Network/GameEvent")]
public class NetworkGameEvent : GameEvent
{
	[SerializeField]
	protected string prefix;
	protected MessageTransmitter transmitter;

	public virtual void ForwardEvent(MessageTransmitter transmitter)
	{
		this.transmitter = transmitter;
	}

	public override void RaiseEvent()
	{
		if (transmitter != null)
			transmitter.WriteToOther(prefix);
		base.RaiseEvent();
	}

	public override void Reset()
	{
		base.Reset();
		transmitter = null;
	}

	public virtual bool Listen(string message)
	{
		if (message.StartsWith(prefix))
		{
			base.RaiseEvent();
			return true;
		}
		return false;
	}
}

public abstract class NetworkGameEvent<T> : GameEvent<T>
{
	[SerializeField]
	protected string prefix;
	protected MessageTransmitter transmitter;

	public virtual void ForwardEvent(MessageTransmitter transmitter)
	{
		this.transmitter = transmitter;
	}

	public override void RaiseEvent(T value)
	{
		if (transmitter != null)
			transmitter.WriteToOther(prefix + CreateEventMessage(value));
		base.RaiseEvent(value);
	}

	public override void Reset()
	{
		base.Reset();
		transmitter = null;
	}

	public virtual bool Listen(string message)
	{
		if (message.StartsWith(prefix))
		{
			base.RaiseEvent(GetEventValue(message.Substring(prefix.Length)));
			return true;
		}
		return false;
	}

	protected abstract string CreateEventMessage(T value);
	protected abstract T GetEventValue(string messageWithoutPrefix);
}
