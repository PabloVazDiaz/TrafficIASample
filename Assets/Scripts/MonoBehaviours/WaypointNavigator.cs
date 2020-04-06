using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    NavigationController controller;
    public Waypoint currentWaypoint;
    
    public int direction;

    void Awake()
    {
        controller = GetComponent<NavigationController>();
        direction = Mathf.RoundToInt(Random.Range(0f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.reachedDestination)
        {
            bool shouldBranch = false;
            if(currentWaypoint.branches!=null && currentWaypoint.branches.Count > 0)
            {
                shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchRatio ? true : false;
            }
            if (shouldBranch)
            {
                currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count - 1)];
                if(direction == 0 && currentWaypoint.NextWaypoint.branchRatio == 1)
                {
                    direction = 1;
                }else if (direction == 1 && currentWaypoint.previousWaypoint.branchRatio == 1)
                {
                    direction = 0;
                }
            }
            else
            {
                if(direction == 0)
                {
                    if (currentWaypoint.NextWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.NextWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.previousWaypoint;
                        direction = 1;
                    }
                }
                else if(direction == 1)
                {
                    if (currentWaypoint.previousWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.previousWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.NextWaypoint;
                        direction = 0;
                    }
                }
            }

            controller.setDestination(currentWaypoint.GetPosition());

        }
    }
}