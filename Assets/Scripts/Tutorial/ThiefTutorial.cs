using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThiefTutorial : MonoBehaviour
{
    [Header("Tutorial texts")]
    [SerializeField]
    [TextArea(3, 10)]
    private string introductionText;

    [SerializeField]
    [TextArea(3, 10)]
    private string basicControlTutorial;

    [SerializeField]
    [TextArea(3, 10)]
    private string commanderTutorialText;

    [SerializeField]
    [TextArea(3, 10)]
    private string findDroneTutorial;

    [SerializeField]
    [TextArea(3, 10)]
    private string droneFeatureTutorial;

    [SerializeField]
    [TextArea(3, 10)]
    private string interactionTutorial;

    [SerializeField]
    [TextArea(3, 10)]
    private string bugTutorial;

    [Header("UI")]
    [SerializeField]
    private TMP_Text tutorialText;

    [Header("Events")]
    [SerializeField]
    private GameEvent OnDroneFireFlare;

    [Header("Objects")]
    [SerializeField]
    private GameObject drone;
    [SerializeField]
    private Camera playerCamera;


    private int stepCounter;

    private void Awake()
    {
        stepCounter = 0;
        NextStep();
    }
    private void Update()
    {
        if(stepCounter == 1 || stepCounter == 2 || stepCounter == 3 || stepCounter == 5)
        {
            if (Input.GetKeyDown(KeyCode.F))
                NextStep();
        }

        if(stepCounter == 5)
        {
            if (Input.GetKeyDown(KeyCode.R))
                OnDroneFireFlare.RaiseEvent();
        }

        if(stepCounter == 4)
        {
            float dotProduct = Vector3.Dot(playerCamera.transform.forward, (drone.transform.position - playerCamera.transform.position).normalized);
            Debug.Log(dotProduct);
            if (dotProduct > 0.97f)
            {
                NextStep();
            }
        }
    }

    public void NextStep()
    {
        switch (++stepCounter)
        {
            case 1: tutorialText.text = introductionText; break;
            case 2: tutorialText.text = basicControlTutorial; break;
            case 3: tutorialText.text = commanderTutorialText; break;
            case 4: tutorialText.text = findDroneTutorial;break;
            case 5: tutorialText.text = droneFeatureTutorial;break;
            case 6:tutorialText.text = interactionTutorial;break;
        }
    }
}
