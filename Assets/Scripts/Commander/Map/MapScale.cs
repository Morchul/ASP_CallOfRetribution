using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class MapScale : MonoBehaviour
{
    [SerializeField]
    protected Camera mainCam;

    [SerializeField]
    protected Map map;

    [SerializeField]
    private float maxScaleGaps;
    [SerializeField]
    private float minScaleGaps;

    [SerializeField]
    private TMP_Text[] numbers;

    //public char[] ScaleSectionSymbols;
    [SerializeField]
    [Tooltip("What is the scale of the whole scale e.g.: 100 =>  scale goes from 0 - 100")]
    private int scaleScale;

    protected float zoomLevel;
    protected float zoomLevelFactor;

    public float ScaleLength { get; set; }

    protected float currentClosestNumberBelow;

    protected Vector3 startPos;

    private float scalePos;
    public float ScalePos
    {
        get => scalePos;
        set
        {
            scalePos = value;
            UpdatePos();
        }
    }

    private void Awake()
    {
        zoomLevel = 1;
        currentClosestNumberBelow = 0;
        zoomLevelFactor = maxScaleGaps / minScaleGaps;
        startPos = transform.position;
    }

    public void UpdateScale()
    {
        //float distanceToMap = Vector3.Distance(mainCam.transform.position, map.transform.position) / Map.MAP_SCALE_POS_RATIO;
        float distanceToMap = Mathf.Abs(mainCam.transform.position.z - map.transform.position.z) / Map.MAP_SCALE_POS_RATIO; //TODO does not work always
        float normalizedVisiblePartOfMap = GetNormalizedVisiblePartOfMap(distanceToMap);
        float bigGapSize = CalcBigGapSize(normalizedVisiblePartOfMap);

        if (bigGapSize < 1 / maxScaleGaps)
        {
            zoomLevel /= zoomLevelFactor;
            Debug.Log("Switch zoom level to: " + zoomLevel);
            bigGapSize = CalcBigGapSize(normalizedVisiblePartOfMap);
            SetScaleAxis(maxScaleGaps * bigGapSize);
            UpdatePos(true);
        }
        else if (bigGapSize > 1 / minScaleGaps)
        {
            zoomLevel *= zoomLevelFactor;
            Debug.Log("Switch zoom level to: " + zoomLevel);
            bigGapSize = CalcBigGapSize(normalizedVisiblePartOfMap);
            SetScaleAxis(maxScaleGaps * bigGapSize);
            UpdatePos(true);
        }
        else
        {
            SetScaleAxis(maxScaleGaps * bigGapSize);
            UpdatePos(false);
        }

        
    }

    public void UpdatePos(bool zoomLevelChange = false)
    {
        float curPos = scaleScale * ScalePos;
        float bigScaleNumber = scaleScale / (maxScaleGaps * zoomLevel);
        float currentShownScale = scaleScale / (zoomLevel * GetScaleAxis());
        float delta = curPos % bigScaleNumber;
        float closestBigScaleNumberBelow = curPos - delta;

        MovePartOfAxis(delta / currentShownScale);

        if (currentClosestNumberBelow != closestBigScaleNumberBelow || zoomLevelChange)
        {
            UpdateNumber(closestBigScaleNumberBelow, bigScaleNumber);
        }
            
    }

    protected void UpdateNumber(float closestNumberBelow, float bigScaleNumber)
    {
        currentClosestNumberBelow = closestNumberBelow;
        float steps = bigScaleNumber / 2;
        float start = closestNumberBelow - bigScaleNumber * 2;

        for(int i = 0; i < numbers.Length; ++i)
        {
            numbers[i].text = (start + steps * i).ToString();
        }
    }

    protected float CalcBigGapSize(float normalizedVisiblePartOfMap)
    {
        return 1 / (normalizedVisiblePartOfMap * zoomLevel * maxScaleGaps);
    }

    protected abstract float GetNormalizedVisiblePartOfMap(float distanceToMap);
    protected abstract void SetScaleAxis(float scale);
    protected abstract float GetScaleAxis();
    protected abstract void MovePartOfAxis(float delta);
}
