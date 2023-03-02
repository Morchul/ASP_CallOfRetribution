using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class GameEventInspector : Editor
{
    private GameEvent _target;

    private void OnEnable()
    {
        _target = (GameEvent)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Raise event"))
        {
            _target.RaiseEvent();
        }
    }
}
