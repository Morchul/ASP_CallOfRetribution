using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vector2Event", menuName = "Events/Vector2Event")]
public class Vector2Event : NetworkGameEvent<Vector2>
{
    protected override string CreateEventMessage(Vector2 value) => value.ToMessageString();

    protected override Vector2 GetEventValue(string messageWithoutPrefix)
    {
        MessageUtility.TryConvertToCoordinates(messageWithoutPrefix, out Vector2 coord);
        return coord;
    }
}
