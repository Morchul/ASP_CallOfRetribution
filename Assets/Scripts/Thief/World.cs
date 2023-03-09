using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField]
    private MapData mapData;

    private void Awake()
    {
        mapData.WorldOffset = transform.position;
    }
}
