using UnityEngine;

public class ExtractionPoint : MonoBehaviour
{
    [Header("Events")]
    [SerializeField]
    private GameEvent OnItemStolen;

    [SerializeField]
    private GameEvent OnMissionFinishedSuccessful;

    [SerializeField]
    private Vector2Event OnExtractionPointActivate;

    private void Awake()
    {
        OnItemStolen.AddListener(() =>
        {
            gameObject.SetActive(true);
            OnExtractionPointActivate.RaiseEvent(transform.position.ToVector2());
        });

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Thief.TAG))
        {
            OnMissionFinishedSuccessful.RaiseEvent();
        }
    }
}
