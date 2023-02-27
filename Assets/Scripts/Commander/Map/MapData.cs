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
    public float MapScaleSizeRatio;

    public Vector3 NormalizedMapPosToWorldPos(Vector2 mapPos)
    {
        return new Vector3(mapPos.x / MapScaleSizeRatio * WorldWidth, 0, mapPos.y / MapScaleSizeRatio * WorldHeight);
    }

    public Vector3 MapCoordinateToWorldPos(float x, float y)
    {
        return new Vector3(x / MapWidth * WorldWidth, 0, y / MapHeight * WorldHeight);
    }

    public Vector3 MapCoordinateToWorldPos(Vector2 coordinates)
    {
        return MapCoordinateToWorldPos(coordinates.x, coordinates.y);
    }

    public Vector2 WorldPosToMapCoordinate(Vector2 worldPos)
    {
        return new Vector2(worldPos.x / WorldWidth * MapWidth, worldPos.y / WorldHeight * MapHeight);
    }

    public Vector2 XZWorldPosToMapPos(Vector2 worldPos)
    {
        return new Vector2(worldPos.x / WorldWidth, worldPos.y / WorldHeight) * MapScaleSizeRatio;
    }
}
