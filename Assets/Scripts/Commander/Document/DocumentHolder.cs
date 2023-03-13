using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentHolder : MonoBehaviour
{
    [SerializeField]
    private float degreeTurnPerDocument;

    [Header("Events")]
    [SerializeField]
    private IntEvent OnNewInformationEvent;

    [Header("Controller")]
    [SerializeField]
    private MissionController missionController;

    [SerializeField]
    private FocusHandler focusHandler;

    public List<Document> Documents { get; private set; }

    private void Awake()
    {
        Documents = new List<Document>();
        OnNewInformationEvent.AddListener(NewDocument);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            focusHandler.StopFocus();
        }
    }

    private void NewDocument(int infoID)
    {
        Document doc = Instantiate(missionController.CurrentMission.GetInformation(infoID), this.transform);
        doc.FocusHandler = focusHandler;
        Documents.Add(doc);

        LayOutDocuments();
    }

    private void LayOutDocuments()
    {
        for(int i = 0; i < Documents.Count; ++i)
        {
            Documents[i].SetOriginPosition(GetRotationEulerAngles(i), GetYPosition(i));
        }
    }

    public Vector3 GetRotationEulerAngles(int i) => new Vector3(0, -((Documents.Count - 1) * degreeTurnPerDocument / 2) + degreeTurnPerDocument * i, 0);
    public float GetYPosition(int i) => 0.02f * i;
}
