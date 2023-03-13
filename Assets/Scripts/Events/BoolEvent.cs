using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoolEvent", menuName = "Events/BoolEvent")]
public class BoolEvent : NetworkGameEvent<bool>
{
    protected override string CreateEventMessage(bool value)
    {
        return value ? "1" : "0";
    }

    protected override bool GetEventValue(string messageWithoutPrefix)
    {
        return messageWithoutPrefix.Equals("1");
    }
}
