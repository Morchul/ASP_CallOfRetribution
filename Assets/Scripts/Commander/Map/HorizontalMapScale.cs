using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMapScale : MapScale
{
    protected override float GetNormalizedVisiblePartOfMap(float distanceToMap)
    {
        return 2.0f * distanceToMap * Mathf.Tan(mainCam.fieldOfView * 0.5f * Mathf.Deg2Rad) * mainCam.aspect / map.MapWidth;
    }

    protected override float GetScaleAxis() => this.transform.localScale.x;

    protected override void MovePartOfAxis(float delta)
    {
        this.transform.position = startPos - new Vector3(delta * mainCam.pixelWidth, 0, 0);
    }

    protected override void SetScaleAxis(float scale)
    {
        this.transform.localScale = new Vector3(scale, 1, 1);
    }
}
