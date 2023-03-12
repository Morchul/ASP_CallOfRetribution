using UnityEngine;

[CreateAssetMenu(fileName = "PosUpdateEvent", menuName = "Events/PosUpdateEvent")]
public class PosUpdateEvent : GameEvent<PosUpdateEvent.PosUpdate>
{

	public void RaiseEvent(char identifier, Vector3 pos)
	{
		RaiseEvent(new PosUpdate(identifier, pos));
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
