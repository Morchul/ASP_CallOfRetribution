using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PosUpdateEvent))]
public class PosUpdateEventInspector : Editor
{
    private PosUpdateEvent _target;
    private string identifier;
    private Vector3 pos;

    private void OnEnable()
    {
        _target = (PosUpdateEvent)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        identifier = EditorGUILayout.TextField("Identifier", identifier);
        pos = EditorGUILayout.Vector3Field("Pos", pos);
        if (GUILayout.Button("Raise event"))
        {
            _target.RaiseEvent(identifier[0], pos);
        }
    }
}
