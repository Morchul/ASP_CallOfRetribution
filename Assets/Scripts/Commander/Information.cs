using UnityEngine;


[CreateAssetMenu(fileName = "Information", menuName = "Information")]
public class Information : ScriptableObject
{
    [SerializeField]
    private int ID;


    [SerializeField]
    private string message;
}
