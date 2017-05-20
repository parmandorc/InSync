using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    // The distance at which the player is considered to have arrived at the target waypoint
    private float WaypointDistanceThreshold = 0.15f;

    [SerializeField]
    // The distance from the current target at which the player can choose the next waypoint
    private float NewWaypointDistanceThreshold = 0.5f;

    private ThirdPersonCharacter m_Character;

    //[SerializeField]
    public Waypoint TargetWaypoint;

	// Use this for initialization
	void Start()
    {
        if (TargetWaypoint == null)
            TargetWaypoint = FindObjectOfType<Waypoint>();

        m_Character = GetComponent<ThirdPersonCharacter>();
	}
	
	// Update is called once per frame
	void Update()
    {
        // The player input for moving
        float waypointInput = Input.GetAxis("Horizontal");

        // Choose a new target waypoint if already at the current target
        float distanceToTarget = Vector3.Distance(transform.position, TargetWaypoint.transform.position);
        if (distanceToTarget < NewWaypointDistanceThreshold)
        {
            if (!Mathf.Approximately(waypointInput, 0.0f))
            {
                if (waypointInput > 0.0f && TargetWaypoint.Right != null)
                {
                    TargetWaypoint = TargetWaypoint.Right;
                }
                else if (waypointInput < 0.0f && TargetWaypoint.Left != null)
                {
                    TargetWaypoint = TargetWaypoint.Left;
                }
            }
        }

        // Get direction vector towards target waypoint
        Vector3 direction = TargetWaypoint.transform.position - transform.position;
        direction.y = 0.0f;
        direction = Vector3.ClampMagnitude(direction, 1.0f);
        if (direction.magnitude < WaypointDistanceThreshold)
            direction = Vector3.zero;

        m_Character.Move(direction, false, Input.GetButtonDown("Jump"));

	}
}
