using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuit : MonoBehaviour
{
    void OnGUI()
    {
        bool quitGame = GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height - 200, 200, 20), "Quit");

        if (quitGame)
        {
            Application.Quit();
            Debug.Log("Quitting");
        }

    }
}
