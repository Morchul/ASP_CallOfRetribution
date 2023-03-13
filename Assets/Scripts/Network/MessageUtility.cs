using UnityEngine;

public static class MessageUtility
{
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

    public static int GetIntAfterPrefix(string prefix, string message)
    {
        if(int.TryParse(message.Substring(prefix.Length), out int res))
        {
            return res;
        }
        return 0;
    }

    //This methods are used because default z would be 0 and y would be used by conversion
    public static Vector2 ToVector2(this Vector3 vec3) => new Vector2(vec3.x, vec3.z);
    public static Vector3 ToVector3(this Vector2 vec2, float yPos = 0) => new Vector3(vec2.x, yPos, vec2.y);
    public static string ToMessageString(this Vector2 vec2) => vec2.x + "/" + vec2.y;
    public static string ToMessageString(this Vector3 vec3) => vec3.x + "/" + vec3.z;
}
