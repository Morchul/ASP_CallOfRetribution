using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableFocusObject : FocusObject
{
    [Header("Move params")]
    [SerializeField]
    private float zoomSpeed = 6f;
    [SerializeField]
    private float moveSpeed = 5f;

    private Vector3 lastPos;

    protected override void Update()
    {
        base.Update();

        if (InFocus && !animate)
        {
            transform.Translate(new Vector3(0, 0, zoomSpeed * Time.deltaTime * Input.mouseScrollDelta.y), Space.World);
        }
    }

    public void OnMouseDown()
    {
        if (InFocus && !animate)
            lastPos = Input.mousePosition;
    }

    public void OnMouseDrag()
    {
        if (InFocus && !animate)
        {
            transform.Translate(new Vector3(Input.mousePosition.x -lastPos.x, Input.mousePosition.y - lastPos.y, 0) * Time.deltaTime * moveSpeed, Space.World);
            lastPos = Input.mousePosition;
        }
    }
}
