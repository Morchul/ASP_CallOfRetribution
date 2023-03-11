using UnityEngine;

public class Information : MonoBehaviour
{
    [SerializeField]
    private Document doc;

    [SerializeField]
    private IntEvent OnNewInformationEvent;

    public void GatherInfo()
    {
        OnNewInformationEvent.RaiseEvent(doc.ID);
    }
}
