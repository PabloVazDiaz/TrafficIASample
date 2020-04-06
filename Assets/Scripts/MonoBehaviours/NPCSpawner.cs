using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{

    public int NPCCount;
    public GameObject NPCPrefab;
    public GameObject Route;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < NPCCount; i++)
        {
            Waypoint startWaypoint = Route.transform.GetChild(Random.Range(0, Route.transform.childCount - 1)).GetComponent<Waypoint>();
            GameObject npc = Instantiate(NPCPrefab);
            npc.transform.position = startWaypoint.transform.position;
            npc.transform.forward = npc.GetComponent<WaypointNavigator>().direction == 0 ? startWaypoint.transform.forward : -startWaypoint.transform.forward;
            
            npc.GetComponent<WaypointNavigator>().currentWaypoint = startWaypoint;
        }
    }

    
}
