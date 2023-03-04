using UnityEngine;

public static class MessageUtility
{
    //Synchronization
    public const string CHAT_PREFIX = "CHAT:"; //Sends a chat message
    public const string SELECT_MISSION_PREFIX = "MISSION:"; //Sends the selected mission
    public const string DRONE_POS_PREFIX = "DRONE_POS:"; //Send to update drone pos
    public const string THIEF_POS_PREFIX = "THIEF_POS:"; //Send to update thief pos
    public const string EXTRACTION_POS_PREFIX = "EXT_POS:"; //Send to give the position of the extraction point
    public const string MISSION_LOADED = "MISSION_LOADED"; //Send by client and host when the mission is loaded like a ready flag
    public const string GAME_READY = "GAME_READY"; //Is send to both if both have send a mission loaded to inform that all members are now ready

    //-------------------------------------------------------
    //------------------Drone commands-----------------------
    //-------------------------------------------------------
    //Client will send either a move / scan / flare command
    public const string MOVE_DRONE_PREFIX = "M_DRONE:";
    public const string SCAN_DRONE = "S_DRONE:";
    public const string FLARE_DRONE = "F_DRONE:";
    //Host will respond with a scan cooldown if the scan is on cooldown or several scan result messages for each scanned object
    public const string SCAN_RESULT_PREFIX = "SCAN_RES:";
    public const string SCAN_COOLDOWN_PREFIX = "SCAN_COOLDOWN:";
    //Send by the host if the connection state of the drone changes (if drone flies into a disturber)
    public const string DRONE_STATE_CHANGE_PREFIX = "DRONE_ST_CH:";

    //-------------------------------------------------------
    //-------------------Bug commands-----------------------
    //-------------------------------------------------------
    //Client will send a bug update the host will verify it and either respond with:
    //                              disturbed (if the bug is disturbed)
    //                              denied (if the bug has no access)
    //                              bug update to verify the change request by client
    public const string BUG_UPDATE_PREFIX = "BUG_UPDATE:";
    public const string BUG_DISTURBED = "BUG_DISTURBED";
    public const string BUG_DENIED = "BUG_DENIED";

    #region Message Helper methods
    public static string CreateExtractionPointPosMessage(Vector3 extractionPointPos)
    {
        return EXTRACTION_POS_PREFIX + extractionPointPos.x + "/" + extractionPointPos.z;
    }

    public static string CreateDronePosMessage(Vector3 dronePos)
    {
        return DRONE_POS_PREFIX + dronePos.x + "/" + dronePos.z;
    }

    public static string CreateThiefPosMessage(Vector3 thiefPos)
    {
        return THIEF_POS_PREFIX + thiefPos.x + "/" + thiefPos.z;
    }

    public static Vector2 GetDronePosFromMessage(string message)
    {
        TryConvertToCoordinates(message.Substring(DRONE_POS_PREFIX.Length), out Vector2 pos);
        return pos;
    }

    public static Vector2 GetThiefPosFromMessage(string message)
    {
        TryConvertToCoordinates(message.Substring(THIEF_POS_PREFIX.Length), out Vector2 pos);
        return pos;
    }

    public static Vector2 GetExtractionPointPosFromMessage(string message)
    {
        TryConvertToCoordinates(message.Substring(EXTRACTION_POS_PREFIX.Length), out Vector2 pos);
        return pos;
    }

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

    public static string CreateMoveDroneMessage(Vector2 coordinates)
    {
        return MOVE_DRONE_PREFIX + coordinates.x + "/" + coordinates.y;
    }

    public static Vector2 GetCoordinates(string message)
    {
        TryConvertToCoordinates(message.Substring(MOVE_DRONE_PREFIX.Length), out Vector2 coord);
        return coord;
    }

    public static string CreateScanResultMessage(Vector3 pos)
    {
        return SCAN_RESULT_PREFIX + pos.x + "/" + pos.z;
    }

    public static Vector2 GetScanResultPos(string message)
    {
        TryConvertToCoordinates(message.Substring(SCAN_RESULT_PREFIX.Length), out Vector2 pos);
        return pos;
    }

    public static string CreateScanCooldownMessage(float cooldown)
    {
        return SCAN_COOLDOWN_PREFIX + cooldown.ToString("0.0");
    }

    public static string CreateDroneStateChangedMessage(bool connected)
    {
        return DRONE_STATE_CHANGE_PREFIX + ((connected) ? "1" : "0");
    }

    public static bool GetDroneState(string message)
    {
        return message.Substring(DRONE_STATE_CHANGE_PREFIX.Length) == "1";
    }
    #endregion

    public static bool TryConvertToCoordinates(string coordinates, out Vector2 coord)
    {
        string[] parts = coordinates.Trim().Split(' ', '/');
        coord = Vector2.zero;
        if (!float.TryParse(parts[0], out float x))
        {
            Debug.LogError("Can't convert coordinate: " + parts[0]);
            return false;
        }
        if (!float.TryParse(parts[1], out float y))
        {
            Debug.LogError("Can't convert coordinate: " + parts[1]);
            return false;
        }
        coord = new Vector2(x, y);
        return true;
    }
}
