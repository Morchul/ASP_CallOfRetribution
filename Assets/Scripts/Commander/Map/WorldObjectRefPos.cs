using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObjectRefPos : MonoBehaviour
{
    [SerializeField]
    private Vector2Event OnPosUpdateEvent;

    [SerializeField]
    private MapData mapData;

    // Start is called before the first frame update
    void Awake()
    {
        OnPosUpdateEvent.AddListener(UpdatePos);
    }

    private void UpdatePos(Vector2 worldPos)
    {
        Vector2 mapPos = mapData.WorldPosToMapPos(worldPos);
        transform.localPosition = new Vector3(mapPos.x, 0, mapPos.y);
    }
}
