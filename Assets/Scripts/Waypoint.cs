using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private Waypoint LeftWaypoint, RightWaypoint;

    public Waypoint Left { get { return LeftWaypoint; } }
    public Waypoint Right { get { return RightWaypoint; } }
}
