using UnityEngine;

public class FinishScreen : MonoBehaviour
{
    [SerializeField]
    private BoolEvent OnMissionFinished;

    [SerializeField]
    private GameObject successfulScreen;

    [SerializeField]
    private GameObject failedScreen;


    void Awake()
    {
        OnMissionFinished.AddListener((successful) =>
        {
            if (successful)
                successfulScreen.SetActive(true);
            else
                failedScreen.SetActive(true);
        });

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
