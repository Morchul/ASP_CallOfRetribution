using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentHolder : MonoBehaviour
{
    [SerializeField]
    private float degreeTurnPerDocument;

    [SerializeField]
    private Transform documentInspectPosition;

    [Header("Events")]
    [SerializeField]
    private IntEvent OnNewInformationEvent;

    [Header("Controller")]
    [SerializeField]
    private MissionController missionController;

    [Header("Prefabs")]
    [SerializeField]
    private Document documentPrefab;

    private int inspectingDocument;

    private List<Document> documents;

    // Start is called before the first frame update
    void Awake()
    {
        documents = new List<Document>();
        OnNewInformationEvent.AddListener(NewDocument);
        inspectingDocument = -1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InspectDocument(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InspectDocument(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnDocument();
        }
    }

    private void NewDocument(int infoID)
    {
        Information information = missionController.CurrentMission.GetInformation(infoID);
        Document doc = Instantiate(documentPrefab, this.transform);
        doc.SetInformation(information);
        documents.Add(doc);

        LayOutDocuments();
    }

    private void LayOutDocuments()
    {
        for(int i = 0; i < documents.Count; ++i)
        {
            if (i == inspectingDocument) continue;

            documents[i].SetRotAndYPos(GetRotationEulerAngles(i), GetYPosition(i));
        }
    }

    public Vector3 GetRotationEulerAngles(int i) => new Vector3(0, -((documents.Count - 1) * degreeTurnPerDocument / 2) + degreeTurnPerDocument * i, 0);
    public float GetYPosition(int i) => 0.02f * i;

    public void InspectDocument(int i)
    {
        if(inspectingDocument >= 0)
        {
            ReturnDocument();
        }
        inspectingDocument = i;

        documents[i].StartAnimation(documentInspectPosition.position, documentInspectPosition.eulerAngles);
    }

    public void ReturnDocument()
    {
        documents[inspectingDocument].StartAnimation(new Vector3(transform.position.x, GetYPosition(inspectingDocument), transform.position.z), GetRotationEulerAngles(inspectingDocument));
        inspectingDocument = -1;
    }
}
