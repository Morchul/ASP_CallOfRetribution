using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatEvent", menuName = "Events/FloatEvent")]
public class FloatEvent : NetworkGameEvent<float>
{
    protected override string CreateEventMessage(float value)
    {
        return value.ToString("0.0");
    }

    protected override float GetEventValue(string messageWithoutPrefix)
    {
        return float.Parse(messageWithoutPrefix);
    }
}
