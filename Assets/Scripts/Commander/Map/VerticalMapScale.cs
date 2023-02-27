using UnityEngine;
using UnityEngine.UI;

public class VerticalMapScale : MapScale
{
    public override float GetPosBoundaries()
    {
        return (1 - normalizedVisiblePartOfMap) / 2 * mapData.MapHeight;
    }

    protected override float GetNormalizedVisiblePartOfMap(float distanceToMap)
    {
        return 2.0f * distanceToMap * Mathf.Tan(mainCam.fieldOfView * 0.5f * Mathf.Deg2Rad) / mapData.MapHeight;
    }

    protected override float GetScaleRange() => mapData.MapHeight;

    protected override float GetScreenScaleLength() => mainCam.pixelHeight;

    protected override void SetScaleLinePos(float delta, float bigLineSteps)
    {
        float start = transform.position.y - delta - bigLineSteps * maxScaleGaps / 2;
        float steps = bigLineSteps * 0.5f;

        for (int i = 0; i < mapScaleLines.Length; ++i)
        {
            mapScaleLines[i].SetPos(new Vector2(transform.position.x, start + steps * i));
        }
    }
}
