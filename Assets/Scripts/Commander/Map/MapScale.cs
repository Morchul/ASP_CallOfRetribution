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
    [Tooltip("What is the scale of the whole scale e.g.: 100 =>  scale goes from 0 - 100")]
    private int scaleScale;
    public int Range => scaleScale;

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
    }

    protected float distanceToMap;
    protected float normalizedVisiblePartOfMap;
    protected float normalizedBigGapSize;
    protected float screenBigGapSize;

    public void UpdateScale()
    {
        bool zoomLevelChange = false;

        float currentDistanceToMap = Mathf.Abs(mainCam.transform.position.z - map.transform.position.z) / Map.MAP_SCALE_POS_RATIO; //TODO does not work always
        if (distanceToMap != currentDistanceToMap) //Needs only a recalc if the distance has changed (zoom has happend)
        {
            distanceToMap = currentDistanceToMap;
            normalizedVisiblePartOfMap = GetNormalizedVisiblePartOfMap(distanceToMap);
            normalizedBigGapSize = CalcNormalizedBigGapSize(normalizedVisiblePartOfMap);
            screenBigGapSize = CalcScreenBigGapSize(normalizedBigGapSize);


            if (normalizedBigGapSize < 1 / maxScaleGaps)
            {
                zoomLevel /= zoomLevelFactor;
                normalizedBigGapSize = CalcNormalizedBigGapSize(normalizedVisiblePartOfMap);
                screenBigGapSize = CalcScreenBigGapSize(normalizedBigGapSize);
                zoomLevelChange = true;
            }
            else if (normalizedBigGapSize > 1 / minScaleGaps)
            {
                zoomLevel *= zoomLevelFactor;
                normalizedBigGapSize = CalcNormalizedBigGapSize(normalizedVisiblePartOfMap);
                screenBigGapSize = CalcScreenBigGapSize(normalizedBigGapSize);
                zoomLevelChange = true;
            }
        }

        float scalePos = scaleScale * NormalizedScalePos;
        float bigLineNumber = scaleScale / (maxScaleGaps * zoomLevel);
        //float currentShownScale = GetScreenScaleLength() / screenBigGapSize * bigLineNumber;
        float currentShownScale = normalizedVisiblePartOfMap * scaleScale;
        float delta = scalePos % bigLineNumber;
        float closestBigScaleNumberBelow = scalePos - delta;

        float screenDelta = GetScreenScaleLength() * delta / currentShownScale;
        //float screenDelta = delta * screenBigGapSize * bigLineNumber;

        SetScaleLinePos(screenDelta, screenBigGapSize);

        //MovePartOfAxis(delta / currentShownScale);

        if (currentClosestNumberBelow != closestBigScaleNumberBelow || zoomLevelChange)
        {
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

    protected float CalcNormalizedBigGapSize(float normalizedVisiblePartOfMap) => 1 / (normalizedVisiblePartOfMap * zoomLevel * maxScaleGaps);
    protected float CalcScreenBigGapSize(float normalizedBigGapSize) => normalizedBigGapSize * GetScreenScaleLength();

    protected abstract float GetNormalizedVisiblePartOfMap(float distanceToMap);

    protected abstract void SetScaleLinePos(float delta, float steps);
    protected abstract float GetScreenScaleLength();
}
