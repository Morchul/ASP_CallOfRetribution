using UnityEngine;

public class ExtractionPoint : MonoBehaviour
{
    [SerializeField]
    private GameEvent OnItemStolen;

    [SerializeField]
    private GameEvent OnMissionFinishedSuccessful;

    private void Awake()
    {
        OnItemStolen.AddListener(() =>
        {
            gameObject.SetActive(true);
            NetworkManager.Instance.Transmitter.WriteToClient(MessageUtility.CreateExtractionPointPosMessage(transform.position));
        });

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnMissionFinishedSuccessful.RaiseEvent();
        }
    }
}
