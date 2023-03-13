using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BugPlacedEvent", menuName = "Events/BugPlacedEvent")]
public class BugUpdateEvent : NetworkGameEvent<BugUpdateEvent.BugUpdate>
{
	public void RaiseEvent(int ID, IBugable.Type type, int status)
	{
		RaiseEvent(new BugUpdate(ID, type, status));
	}

	protected override string CreateEventMessage(BugUpdate value)
	{
		return value.ID + "/" + (int)value.Type + "/" + value.Status;
	}

	protected override BugUpdate GetEventValue(string messageWithoutPrefix)
	{
		string[] values = messageWithoutPrefix.Split('/');
		return new BugUpdate
		(
			int.Parse(values[0]),
			(IBugable.Type)int.Parse(values[1]),
			int.Parse(values[2])
		);
	}

	public struct BugUpdate
	{
		public int ID;
		public IBugable.Type Type;
		public int Status;

		public BugUpdate(int id, IBugable.Type type, int status)
		{
			ID = id;
			Type = type;
			Status = status;
		}
	}
}
