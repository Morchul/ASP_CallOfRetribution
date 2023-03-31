using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "MapData")]
public class MapData : ScriptableObject
{
    public float WorldWidth;
    public float WorldHeight;
    public float MapWidth;
    public float MapHeight;
    public float MapSizeX;
    public float MapSizeY;

    [Tooltip("With a scale of 1 how big is the map")]
    //Leads from a normalized position to the local Position
    public float MapScaleSizeRatio;

    public Vector3 WorldOffset = Vector3.zero;

    public Vector3 MapPosToWorldPos(Vector2 mapPos)
    {
        return new Vector3(mapPos.x / MapScaleSizeRatio * WorldWidth, 0, mapPos.y / MapScaleSizeRatio * WorldHeight) + WorldOffset;
    }

    public Vector3 MapCoordinateToWorldPos(float x, float y)
    {
        return new Vector3(x / MapWidth * WorldWidth, 0, y / MapHeight * WorldHeight) + WorldOffset;
    }

    public Vector3 MapCoordinateToWorldPos(Vector2 coordinates)
    {
        return MapCoordinateToWorldPos(coordinates.x, coordinates.y);
    }

    public Vector2 WorldPosToMapCoordinate(Vector2 worldPos)
    {
        worldPos = new Vector2(worldPos.x - WorldOffset.x, worldPos.y - WorldOffset.z);

        return new Vector2(worldPos.x / WorldWidth * MapWidth, worldPos.y / WorldHeight * MapHeight);
    }

    public Vector2 WorldPosToMapPos(Vector3 worldPos) => XZWorldPosToMapPos(worldPos.ToVector2());

    public Vector2 XZWorldPosToMapPos(Vector2 worldPos)
    {
        worldPos -= WorldOffset.ToVector2();

        return new Vector2(Mathf.Clamp(worldPos.x / WorldWidth, 0, 1), Mathf.Clamp(worldPos.y / WorldHeight, 0, 1)) * MapScaleSizeRatio;
    }

    public Vector2 MapCoordinateToMapPos(Vector2 mapCoordinate)
    {
        return new Vector2(mapCoordinate.x / MapWidth, mapCoordinate.y / MapHeight) * MapScaleSizeRatio;
    }
}
