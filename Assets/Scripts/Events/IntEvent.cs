using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntEvent", menuName = "Events/IntEvent")]
public class IntEvent : NetworkGameEvent<int>
{
    protected override string CreateEventMessage(int value)
    {
        return value.ToString();
    }

    protected override int GetEventValue(string messageWithoutPrefix)
    {
        return int.Parse(messageWithoutPrefix);
    }
}
