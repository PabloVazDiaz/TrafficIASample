using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class WaypointManagerWindow : EditorWindow
{
 
    [MenuItem("Tools/Waypoint Editor")]
    public static void Open()
    {
        GetWindow<WaypointManagerWindow>();
    }

    public Transform waypointRoot;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);
        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        if (waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Root trasform must be selected. Please asign root trasform", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }

    private void DrawButtons()
    {
        if (GUILayout.Button("CreateWaypoint"))
        {
            CreateWaypoint();
        }
    }

    private void CreateWaypoint()
    {
        GameObject waypointObject = new GameObject($"Waypoint {waypointRoot.childCount}", typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);
        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();

        if (waypointRoot.childCount > 1)
        {
            waypoint.previousWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypoint>();
            waypoint.previousWaypoint.NextWaypoint = waypoint;

            waypoint.transform.position = waypoint.previousWaypoint.transform.position;
            waypoint.transform.rotation = waypoint.previousWaypoint.transform.rotation;
        }


        Selection.activeGameObject = waypointObject;

    }
}
