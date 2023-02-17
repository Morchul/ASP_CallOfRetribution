using UnityEngine;
using TMPro;

public class Document : MovableFocusObject, IData
{
    //private Information info;
    [Header("Doc info")]
    [SerializeField]
    private int id;
    public int ID => id;

    [SerializeField]
    private bool startInfo;
    public bool StartInfo => startInfo;

    //[SerializeField]
    //private TMP_Text text;

    /*private Vector3 startPos;
    private Vector3 startRot;
    private Vector3 targetPos;
    private Vector3 targetRot;
    private bool animate;
    private float timer;
    private float animationDuration = 2;

    private void Update()
    {
        if(animate)
        {
            if (timer < 1)
            {
                transform.position = Vector3.Lerp(startPos, targetPos, timer);
                transform.eulerAngles = Vector3.Lerp(startRot, targetRot, timer);
                timer += Time.deltaTime / animationDuration;
            }
            else
                animate = false;
        }
    }

    public void StartAnimation(Vector3 targetPos, Vector3 targetRot)
    {
        startPos = transform.position;
        startRot = transform.eulerAngles;
        this.targetPos = targetPos;
        this.targetRot = targetRot;
        timer = 0;
        animate = true;
    }
    */

    /*public void SetInformation(Information information)
    {
        info = information;
        text.text = info.Message;
    }*/
}
