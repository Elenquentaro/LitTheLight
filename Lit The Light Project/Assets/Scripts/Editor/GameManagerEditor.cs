using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Clear progress"))
        {
            SaveLoader.SaveProgress(new Progress());
        }

        if (GUILayout.Button("Log current progress to console"))
        {
            Debug.Log(SaveLoader.LoadProgress());
        }

        DrawDefaultInspector();
    }
}
