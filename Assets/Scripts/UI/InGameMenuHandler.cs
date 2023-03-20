using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject inGameMenuUI;

    private bool isActive;

    private void Awake()
    {
        isActive = true;
        ShowHide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowHide();
        }
    }

    public void ShowHide()
    {
        isActive = !isActive;
        inGameMenuUI.SetActive(isActive);

        if(isActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
