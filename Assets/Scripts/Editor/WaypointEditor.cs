using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class WaypointEditor 
{

    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
    {
        //Waypoint Gizmo
        if ((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.yellow * 0.5f;
        }

        Gizmos.DrawSphere(waypoint.transform.position, 0.2f);

        // Width gizmo
        Gizmos.color = Color.white;

        Gizmos.DrawLine(waypoint.transform.position + waypoint.transform.right * waypoint.width / 2f,
            waypoint.transform.position - waypoint.transform.right * waypoint.width / 2f);

        //Previous and next Waypoint gizmo

        Gizmos.color = Color.red;
        if (waypoint.previousWaypoint != null)
        {
            var offset = waypoint.transform.position + waypoint.transform.right * waypoint.width / 2f;
            var offsetTo = waypoint.previousWaypoint.transform.position + waypoint.previousWaypoint.transform.right * waypoint.previousWaypoint.width / 2f;
            Gizmos.DrawLine(offset, offsetTo);
        }
        Gizmos.color = Color.green;
        if (waypoint.NextWaypoint != null)
        {
            var offset = waypoint.transform.position - waypoint.transform.right * waypoint.width / 2f;
            var offsetTo = waypoint.NextWaypoint.transform.position - waypoint.NextWaypoint.transform.right * waypoint.NextWaypoint.width / 2f;
            Gizmos.DrawLine(offset, offsetTo);
        }

        if (waypoint.branches != null)
        {
            Gizmos.color = Color.blue;
            foreach (Waypoint branch in waypoint.branches)
            {
                Gizmos.DrawLine(waypoint.transform.position, branch.transform.position);
            }
        }
    }
}
