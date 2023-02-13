using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour
{
    [Header("Events")]
    [SerializeField]
    private IntEvent OnNewInformationEvent;

    [Header("Controller")]
    [SerializeField]
    private MissionController missionController;

    [Header("Prefabs")]
    [SerializeField]
    private Document documentPrefab;

    private List<IBugable> buggedObjects;

    private void Awake()
    {
        buggedObjects = new List<IBugable>();
        OnNewInformationEvent.AddListener(NewDocument);
    }

    private void NewDocument(int infoID)
    {
        Information information = missionController.CurrentMission.GetInformation(infoID);
        Document doc = Instantiate(documentPrefab);
        doc.SetInformation(information);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (hit.transform.gameObject.CompareTag("Radio"))
                {
                    hit.transform.gameObject.GetComponent<Radio>().TurnOnOff();
                }
            }
        }
    }
}
