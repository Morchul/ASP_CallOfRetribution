using UnityEngine;

public class FinishScreen : MonoBehaviour
{
    [SerializeField]
    private GameEvent OnMissionFinishedSuccessfully;

    [SerializeField]
    private GameEvent OnMissionFailed;

    [SerializeField]
    private GameObject successfulScreen;

    [SerializeField]
    private GameObject failedScreen;


    void Awake()
    {
        OnMissionFinishedSuccessfully.AddListener(() => successfulScreen.SetActive(true));
        OnMissionFailed.AddListener(() => failedScreen.SetActive(true));
    }
}
