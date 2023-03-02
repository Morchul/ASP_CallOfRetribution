using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Vector2Event))]
public class Vector2EventInspector : Editor
{
    private Vector2Event _target;
    private Vector2 param;

    private void OnEnable()
    {
        _target = (Vector2Event)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        param = EditorGUILayout.Vector2Field("Param", param);
        if (GUILayout.Button("Raise event"))
        {
            _target.RaiseEvent(param);
        }
    }
}
