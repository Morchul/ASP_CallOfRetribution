using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StringEvent", menuName = "Events/StringEvent")]
public class StringEvent : NetworkGameEvent<string>
{
    protected override string CreateEventMessage(string value) => value;

    protected override string GetEventValue(string messageWithoutPrefix) => messageWithoutPrefix;
}
