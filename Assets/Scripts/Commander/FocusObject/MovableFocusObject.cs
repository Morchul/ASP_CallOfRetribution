using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableFocusObject : FocusObject
{
    [Header("Move params")]
    [SerializeField]
    private float zoomSpeed = 6f;
    [SerializeField]
    protected float moveSpeed = 5f;

    private Vector3 lastPos;

    protected override void Update()
    {
        base.Update();

        if (InFocus && !animate)
        {
            float scroll = Input.mouseScrollDelta.y;
            if (scroll != 0)
                Zoom(zoomSpeed * Time.deltaTime * scroll);
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
            Move(Input.mousePosition - lastPos);
            //Move(new Vector2(Input.mousePosition.x - lastPos.x, Input.mousePosition.y - lastPos.y));
            lastPos = Input.mousePosition;
        }
    }

    protected virtual void Zoom(float zoom)
    {
        transform.Translate(new Vector3(0, zoom, 0), Space.Self);
    }

    protected virtual void Move(Vector2 delta)
    {
        if (Time.deltaTime > 0.1)
        {
            Debug.Log("Time spike: " + Time.deltaTime);
            delta *= 0.1f * moveSpeed;
        }
        else
        {
            delta *= Time.deltaTime * moveSpeed;
        }

        transform.Translate(new Vector3(delta.x, 0, delta.y), Space.Self);
    }
}
