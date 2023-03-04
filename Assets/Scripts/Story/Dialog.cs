using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Story/Dialog")]
public class Dialog : ScriptableObject
{
    [TextArea(5,10)]
    public string Text;

    public Sprite rightImage;
    public Sprite leftImage;
}
