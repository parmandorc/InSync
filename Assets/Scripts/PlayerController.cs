using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(1,4)]
    private int PlayerID = 1;

    [SerializeField]
    private Color PlayerColor;

    [SerializeField]
    // The distance at which the player is considered to have arrived at the target waypoint
    private float WaypointDistanceThreshold = 0.15f;

    [SerializeField]
    // The distance from the current target at which the player can choose the next waypoint
    private float NewWaypointDistanceThreshold = 0.5f;

    [SerializeField]
    private PianoKey StartingKey;

    private ThirdPersonCharacter m_Character;

    // Getters
    public PianoKey SelectedKey { get; private set; }
    public Color playerColor { get { return PlayerColor; } }
    public int playerID { get { return PlayerID; } }

	// Use this for initialization
	void Start()
    {
        SelectKey((StartingKey != null) ? StartingKey : FindObjectOfType<PianoKey>());

        m_Character = GetComponent<ThirdPersonCharacter>();
	}
	
	// Update is called once per frame
	void Update()
    {
        float waypointInput = Input.GetAxis("Horizontal" + PlayerID);
       
        // Choose a new target waypoint if already at the current target
        float distanceToTarget = Vector3.Distance(transform.position, SelectedKey.Waypoint.position);
        if (distanceToTarget < NewWaypointDistanceThreshold)
        {
            if (!Mathf.Approximately(waypointInput, 0.0f))
            {
                if (waypointInput > 0.0f && SelectedKey.Next != null)
                {
                    SelectKey(SelectedKey.Next);
                }
                else if (waypointInput < 0.0f && SelectedKey.Prev != null)
                {
                    SelectKey(SelectedKey.Prev);
                }
            }
        }

        // Get direction vector towards target waypoint
        Vector3 direction = SelectedKey.Waypoint.position - transform.position;
        direction.y = 0.0f;
        direction = Vector3.ClampMagnitude(direction, 1.0f);
        if (direction.magnitude < WaypointDistanceThreshold)
            direction = Vector3.zero;

        m_Character.Move(direction, false, Input.GetButtonDown("Jump" + PlayerID));
	}

    private void SelectKey(PianoKey newKey)
    {
        if (SelectedKey != null) SelectedKey.OnDeselect(this);
        SelectedKey = newKey;
        SelectedKey.OnSelect(this);
    }
}
