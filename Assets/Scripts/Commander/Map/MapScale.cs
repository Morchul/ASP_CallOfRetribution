using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapScale : MonoBehaviour
{
    [SerializeField]
    protected Camera mainCam;

    [SerializeField]
    protected Map map;

    [SerializeField]
    protected float maxScaleGaps;
    [SerializeField]
    protected float minScaleGaps;

    [SerializeField]
    private MapScaleLine mapScaleLinePrefab;
    protected MapScaleLine[] mapScaleLines;

    //public char[] ScaleSectionSymbols;
    [SerializeField]
    protected MapData mapData;
    private float scaleRange;

    [Header("Boundaries")]
    [SerializeField]
    private int maxZoomLevel;

    protected float zoomLevel;
    protected float zoomLevelFactor;

    public float ScaleLength { get; set; }

    protected float currentClosestNumberBelow;

    private float normalizedScalePos;
    public float NormalizedScalePos
    {
        get => normalizedScalePos;
        set
        {
            normalizedScalePos = value;
            UpdateScale();
        }
    }

    private void Awake()
    {
        zoomLevel = 1;
        currentClosestNumberBelow = 0;
        zoomLevelFactor = maxScaleGaps / minScaleGaps;
        distanceToMap = 0;

        mapScaleLines = new MapScaleLine[(int)maxScaleGaps * 2 + 1];
        for(int i = 0; i < mapScaleLines.Length; ++i)
        {
            mapScaleLines[i] = Instantiate(mapScaleLinePrefab, this.transform);
            if (i % 2 == 1)
                mapScaleLines[i].SmallLine();
        }

        scaleRange = GetScaleRange();
        maxDistance = GetMaxDistance();
    }

    protected float distanceToMap;
    protected float normalizedVisiblePartOfMap;
    protected float normalizedBigGapSize;
    protected float screenBigGapSize;
    protected bool zoomLevelChanged;
    protected float maxDistance;

    public void UpdateZoom()
    {
        float currentDistanceToMap = Mathf.Abs(mainCam.transform.position.z - map.transform.position.z); //TODO does not work always
        if (distanceToMap != currentDistanceToMap) //Needs only a recalc if the distance has changed (zoom has happend)
        {
            distanceToMap = Mathf.Min(maxDistance, currentDistanceToMap);
            normalizedVisiblePartOfMap = GetNormalizedVisiblePartOfMap(distanceToMap);
            normalizedBigGapSize = CalcNormalizedBigGapSize(normalizedVisiblePartOfMap);
            screenBigGapSize = CalcScreenBigGapSize(normalizedBigGapSize);


            if (normalizedBigGapSize < 1 / maxScaleGaps)
            {
                zoomLevel /= zoomLevelFactor;
                normalizedBigGapSize = CalcNormalizedBigGapSize(normalizedVisiblePartOfMap);
                screenBigGapSize = CalcScreenBigGapSize(normalizedBigGapSize);
                zoomLevelChanged = true;
            }
            else if (normalizedBigGapSize > 1 / minScaleGaps)
            {
                zoomLevel *= zoomLevelFactor;
                normalizedBigGapSize = CalcNormalizedBigGapSize(normalizedVisiblePartOfMap);
                screenBigGapSize = CalcScreenBigGapSize(normalizedBigGapSize);
                zoomLevelChanged = true;
            }
        }
    }

    public void UpdateScale()
    {
        float scalePos = scaleRange * NormalizedScalePos;
        float bigLineNumber = scaleRange / (maxScaleGaps * zoomLevel);
        float currentShownScale = normalizedVisiblePartOfMap * scaleRange;
        float delta = scalePos % bigLineNumber;
        float closestBigScaleNumberBelow = scalePos - delta;

        float screenDelta = GetScreenScaleLength() * delta / currentShownScale;

        SetScaleLinePos(screenDelta, screenBigGapSize);

        if (currentClosestNumberBelow != closestBigScaleNumberBelow || zoomLevelChanged)
        {
            zoomLevelChanged = false;
            UpdateNumber(closestBigScaleNumberBelow, bigLineNumber);
        }
    }

    protected void UpdateNumber(float closestNumberBelow, float bigScaleNumber)
    {
        currentClosestNumberBelow = closestNumberBelow;
        float steps = bigScaleNumber / 2;
        float start = closestNumberBelow - bigScaleNumber * 2;

        for(int i = 0; i < mapScaleLines.Length; ++i)
        {
            mapScaleLines[i].SetText((start + steps * i).ToString());
        }
    }

    public abstract float GetPosBoundaries();
    public virtual float SetZoomBoundaries(float zoom)
    {
        //Zoom in
        if(zoom > 0 && maxZoomLevel <= zoomLevel)
        {
            return 0;
        }
        //zoom out
        if(zoom < 0 && distanceToMap >= maxDistance)
        {
            return 0;
        }

        return zoom;
    }

    protected float CalcNormalizedBigGapSize(float normalizedVisiblePartOfMap) => 1 / (normalizedVisiblePartOfMap * zoomLevel * maxScaleGaps);
    protected float CalcScreenBigGapSize(float normalizedBigGapSize) => normalizedBigGapSize * GetScreenScaleLength();

    protected abstract float GetNormalizedVisiblePartOfMap(float distanceToMap);

    protected abstract void SetScaleLinePos(float delta, float steps);
    protected abstract float GetScreenScaleLength();
    protected abstract float GetScaleRange();
    protected abstract float GetMaxDistance();
}
