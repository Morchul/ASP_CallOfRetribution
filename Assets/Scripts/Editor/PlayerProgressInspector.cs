using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProgressController))]
public class PlayerProgressInspector : Editor
{
    private ProgressController _target;

    private void OnEnable()
    {
        _target = (ProgressController)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Reset progress"))
        {
            _target.SetCurrentProgress(0);
        }
    }
}
