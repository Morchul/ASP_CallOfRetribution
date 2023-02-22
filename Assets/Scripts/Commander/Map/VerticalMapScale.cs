using UnityEngine;
using UnityEngine.UI;

public class VerticalMapScale : MapScale
{
    protected override float GetNormalizedVisiblePartOfMap(float distanceToMap)
    {
        return 2.0f * distanceToMap * Mathf.Tan(mainCam.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    protected override float GetScaleAxis() => this.transform.localScale.y;

    protected override void MovePartOfAxis(float delta)
    {
        this.transform.position = startPos - new Vector3(0, delta * mainCam.pixelHeight, 0);
    }

    protected override void SetScaleAxis(float scale)
    {
        this.transform.localScale = new Vector3(1, scale, 1);
    }
}
