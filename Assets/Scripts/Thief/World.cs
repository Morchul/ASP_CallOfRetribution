using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField]
    private MapData mapData;

    //TODO:
    // Set Map data
    // Place world object
    // Insert map by commander
    // Copy drone
    // Add thief interactable
    // add thief test script

    private void Awake()
    {
        mapData.WorldOffset = transform.position;
    }
}
