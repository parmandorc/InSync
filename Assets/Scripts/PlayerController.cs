using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerController : MonoBehaviour
{
    private ThirdPersonCharacter m_Character;

	// Use this for initialization
	void Start()
    {
        m_Character = GetComponent<ThirdPersonCharacter>();
	}
	
	// Update is called once per frame
	void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        // Clamp direction magnitude to 1
        if (direction.magnitude > 1.0f)
        {
            direction.Normalize();
        }

        m_Character.Move(direction, false, Input.GetButtonDown("Jump"));
	}
}
