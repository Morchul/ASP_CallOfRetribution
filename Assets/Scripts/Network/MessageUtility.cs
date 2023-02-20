using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MessageUtility
{
    public const string CHAT_PREFIX = "CHAT:";
    public const string SELECT_MISSION_PREFIX = "MISSION:";
    public const string BUG_UPDATE_PREFIX = "BUG_UPDATE:";

    public static string CreateChatMessage(string message)
    {
        return CHAT_PREFIX + NetworkManager.Instance.ConnectionHandler.GetChatName() + ": " + message;
    }

    public static string GetChatMessage(string message)
    {
        return message.Substring(CHAT_PREFIX.Length);
    }

    public static string CreateSelectMissionMessage(int missionID)
    {
        return SELECT_MISSION_PREFIX + missionID;
    }

    public static int GetMissionID(string message)
    {
        return int.Parse(message.Substring(SELECT_MISSION_PREFIX.Length));
    }

    public static string CreateBugUpdateMessage(int bugID, IBugable.Type type, int status)
    {
        return BUG_UPDATE_PREFIX + bugID + "/" + (int)type + "/" + status;
    }

    public static int[] GetBugUpdateValues(string message)
    {
        string[] values = message.Substring(BUG_UPDATE_PREFIX.Length).Split('/');
        return new int[]
        {
            int.Parse(values[0]),
            int.Parse(values[1]),
            int.Parse(values[2])
        };
    }
}
