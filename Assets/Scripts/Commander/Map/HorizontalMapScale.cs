using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMapScale : MapScale
{
    public override float GetPosBoundaries()
    {
        return (1 - normalizedVisiblePartOfMap) / 2 * mapData.MapSizeX;
    }

    protected override float GetNormalizedVisiblePartOfMap(float distanceToMap)
    {
        return 2.0f * distanceToMap * Mathf.Tan(mainCam.fieldOfView * 0.5f * Mathf.Deg2Rad) * mainCam.aspect / mapData.MapSizeX;
    }

    protected override float GetMaxDistance()
    {
        return 1 / (2.0f * Mathf.Tan(mainCam.fieldOfView * 0.5f * Mathf.Deg2Rad) * mainCam.aspect / mapData.MapSizeX);
    }

    protected override float GetScaleRange() => mapData.MapWidth;

    protected override float GetScreenScaleLength() => mainCam.pixelWidth;

    protected override void SetScaleLinePos(float delta, float bigLineSteps)
    {
        float start = transform.position.x - delta - bigLineSteps * (int)(maxScaleGaps / 2);
        float steps = bigLineSteps * 0.5f;

        for (int i = 0; i < mapScaleLines.Length; ++i)
        {
            mapScaleLines[i].SetPos(new Vector2(start + steps * i, transform.position.y));
        }
    }
}
