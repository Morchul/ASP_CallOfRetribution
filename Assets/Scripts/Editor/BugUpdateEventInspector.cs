using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BugUpdateEvent))]
public class BugUpdateEventInspector : Editor
{
    private BugUpdateEvent _target;
    private int bugID;
    private IBugable.Type type;
    private int status;

    private void OnEnable()
    {
        _target = (BugUpdateEvent)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        bugID = EditorGUILayout.IntField("BugID", bugID);
        type = (IBugable.Type)EditorGUILayout.EnumFlagsField("Type", type);
        status = EditorGUILayout.IntField("Status", status);
        if (GUILayout.Button("Raise event"))
        {
            _target.RaiseEvent(bugID, type, status);
        }
    }
}
