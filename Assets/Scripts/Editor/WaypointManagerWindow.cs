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
        if(Selection.activeGameObject !=null && Selection.activeGameObject.GetComponent<Waypoint>())
        {
            if(GUILayout.Button("Create Waypoint Before"))
            {
                CreateWaypointBefore();
            }
            if (GUILayout.Button("Create Waypoint After"))
            {
                CreateWaypointAfter();
            }
            if (GUILayout.Button("Delete Waypoint"))
            {
                DeleteWaypoint();
            }
        }
    }

    private void DeleteWaypoint()
    {
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        if (selectedWaypoint.NextWaypoint != null)
        {
            selectedWaypoint.NextWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
        }
        if (selectedWaypoint.previousWaypoint != null)
        {
            selectedWaypoint.previousWaypoint.NextWaypoint = selectedWaypoint.NextWaypoint;
        }
        DestroyImmediate(selectedWaypoint.gameObject);
    }

    private void CreateWaypointAfter()
    {
        GameObject waypointObject = new GameObject($"Waypopint {waypointRoot.childCount}", typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);
        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();

        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.rotation = selectedWaypoint.transform.rotation;

        if (selectedWaypoint.NextWaypoint != null)
        {
            waypoint.NextWaypoint = selectedWaypoint.NextWaypoint;
            selectedWaypoint.NextWaypoint.previousWaypoint = waypoint;
        }
        waypoint.previousWaypoint = selectedWaypoint;
        selectedWaypoint.NextWaypoint = waypoint;

        waypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex() + 1);

        Selection.activeGameObject = waypointObject;
    }

    private void CreateWaypointBefore()
    {
        GameObject waypointObject = new GameObject($"Waypopint {waypointRoot.childCount}", typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);
        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();

        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.rotation = selectedWaypoint.transform.rotation;

        if (selectedWaypoint.previousWaypoint != null)
        {
            waypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
            selectedWaypoint.previousWaypoint.NextWaypoint = waypoint;
        }
        waypoint.NextWaypoint = selectedWaypoint;
        selectedWaypoint.previousWaypoint = waypoint;

        waypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

        Selection.activeGameObject = waypointObject;
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
