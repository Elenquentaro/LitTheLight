using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Eye))]
public class EyeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Set current position as areal"))
        {
            (target as Eye).SetArealPosition();
            EditorUtility.SetDirty(target);
        }

        DrawDefaultInspector();
    }
}
