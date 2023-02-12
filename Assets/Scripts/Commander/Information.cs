using UnityEngine;


[CreateAssetMenu(fileName = "Information", menuName = "Mission/Information")]
public class Information : ScriptableObject
{
    public int ID;
    public bool StartInfo;

    [TextArea(10, 50)]
    public string Message;
}
