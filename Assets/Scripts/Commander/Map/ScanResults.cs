using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanResults : MonoBehaviour
{
    [SerializeField]
    private Vector2Event OnGuardScanned;

    [SerializeField]
    private MapData mapData;

    [SerializeField]
    private ScanResultObject ScanObjectPrefab;

    private List<ScanResultObject> scanObjectPool;

    private void Awake()
    {
        OnGuardScanned.AddListener(NewScan);
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
