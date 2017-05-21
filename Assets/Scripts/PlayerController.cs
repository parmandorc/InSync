using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerController : MonoBehaviour
{

    string EnterButton;
    string MoveAxis; 

    [SerializeField, Range(1, 4)]
    private int PlayerID = 1;

    [SerializeField]
    private Color PlayerColor;

    [SerializeField]
    // The distance at which the player is considered to have arrived at the target waypoint
    private float WaypointDistanceThreshold = 0.15f;

    [SerializeField]
    // The minimum time that has to pass to change the selected key
    private float KeyChangeMinTime = 0.25f;

    [SerializeField]
    private PianoKey StartingKey;

    private ThirdPersonCharacter m_Character;

    private float m_TimerForWaypointChange;

    private JumpOnKey m_JumpComponent;

    // Getters
    public PianoKey SelectedKey { get; private set; }
    public Color playerColor { get { return PlayerColor; } }
    public int playerID { get { return PlayerID; } }
    public string getEnterButton { get { return EnterButton; } }

    // Use this for initialization
    void Start()
    {
        SelectKey((StartingKey != null) ? StartingKey : FindObjectOfType<PianoKey>());

        m_Character = GetComponent<ThirdPersonCharacter>();
        m_JumpComponent = GetComponent<JumpOnKey>();
    }

    // Update is called once per frame
    void Update()
    {
        float waypointInput = Input.GetAxis(MoveAxis);

        // Choose a new target waypoint
        if (m_JumpComponent.CurrentJumpState == JumpOnKey.jumpStates.notJumping && !Mathf.Approximately(waypointInput, 0))
        {
            if (m_TimerForWaypointChange <= 0.0f)
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

            m_TimerForWaypointChange = Mathf.Max(m_TimerForWaypointChange - Time.deltaTime, 0.0f);
        }
        else
        {
            m_TimerForWaypointChange = 0.0f;
        }

        // Get direction vector towards target waypoint
        Vector3 direction = Vector3.forward;
        if (m_JumpComponent.CurrentJumpState == JumpOnKey.jumpStates.notJumping)
        {
            direction = SelectedKey.Waypoint.position - transform.position;
            direction.y = 0.0f;
            direction = Vector3.ClampMagnitude(direction, 1.0f);
            if (direction.magnitude < WaypointDistanceThreshold)
                direction = Vector3.zero;
        }

        m_Character.Move(direction, false, Input.GetButtonDown(EnterButton));
    }

    private void SelectKey(PianoKey newKey)
    {
        if (SelectedKey != null) SelectedKey.OnDeselect(this);
        SelectedKey = newKey;
        SelectedKey.OnSelect(this);
        m_TimerForWaypointChange = KeyChangeMinTime;
    }

    public void setupCharacter(string _enterButton, string _moveAxis, Color _playerColor, PianoKey _key)
    {
        print(_enterButton);
        print(_moveAxis);
        EnterButton = _enterButton;
        MoveAxis = _moveAxis;
        PlayerColor = _playerColor;
        StartingKey = _key;
    }

}
