using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanResults : MonoBehaviour
{
    [Header("Events")]
    [SerializeField]
    private Vector2Event OnGuardScanned;
    [SerializeField]
    private Vector2Event OnExtractionPointActivated;

    [Header("Data")]
    [SerializeField]
    private MapData mapData;

    [Header("Objects")]
    [SerializeField]
    private ScanResultObject ScanObjectPrefab;
    [SerializeField]
    private GameObject extractionPointRefObject;
    

    private List<ScanResultObject> scanObjectPool;

    private void Awake()
    {
        OnGuardScanned.AddListener(NewScan);
        OnExtractionPointActivated.AddListener((worldPos) => extractionPointRefObject.transform.position = mapData.XZWorldPosToMapPos(worldPos));
        scanObjectPool = new List<ScanResultObject>();
    }


    private void NewScan(Vector2 pos)
    {
        Vector2 mapPos = mapData.XZWorldPosToMapPos(pos);
        if (scanObjectPool.Count > 0)
        {
            scanObjectPool[scanObjectPool.Count - 1].SetPosition(mapPos);
            scanObjectPool.RemoveAt(scanObjectPool.Count - 1);
        }
        else
        {
            ScanResultObject obj = Instantiate(ScanObjectPrefab, transform);
            obj.Pool = this;
            obj.SetPosition(mapPos);
        }
    }

    public void Return(ScanResultObject scanResultObject)
    {
        scanResultObject.SetInaktiv();
        scanObjectPool.Add(scanResultObject);
    }
}
