using UnityEngine;

[CreateAssetMenu(fileName = "PosUpdateEvent", menuName = "Events/PosUpdateEvent")]
public class PosUpdateEvent : NetworkGameEvent<PosUpdateEvent.PosUpdate>
{

	public void RaiseEvent(char identifier, Vector3 pos)
	{
		RaiseEvent(new PosUpdate(identifier, pos));
	}

	protected override string CreateEventMessage(PosUpdate value)
	{
		return value.Identifier + value.Pos.ToMessageString();
	}

	protected override PosUpdate GetEventValue(string messageWithoutPrefix)
	{
		MessageUtility.TryConvertToCoordinates(messageWithoutPrefix.Substring(1), out Vector2 pos);
		return new PosUpdate(messageWithoutPrefix[0], pos.ToVector3());
	}

	public struct PosUpdate
	{
		public char Identifier;
		public Vector3 Pos;

		public PosUpdate(char id, Vector3 pos)
		{
			Identifier = id;
			Pos = pos;
		}
	}
}
