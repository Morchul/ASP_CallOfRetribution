using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour
{
    [SerializeField]
    private DocumentHolder documentHolder;

    /*private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (hit.transform.gameObject.CompareTag("Radio"))
                {
                    hit.transform.gameObject.GetComponent<Radio>().TurnOnOff();
                }
            }
        }
    }*/
}
