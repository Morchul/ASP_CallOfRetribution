using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NetworkGameEvent))]
public class GameEventInspector : Editor
{
    private NetworkGameEvent _target;

    private void OnEnable()
    {
        _target = (NetworkGameEvent)target;
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
