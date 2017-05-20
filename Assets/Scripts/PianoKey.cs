using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoKey : MonoBehaviour
{
    [SerializeField]
    private Material SelectedMaterial;

    [SerializeField]
    private Transform KeyWaypoint;

    [SerializeField]
    private PianoKey PrevKey, NextKey;

    private Renderer m_Renderer;

    // The default material of the key
    private Material m_DefaultMaterial;

    // Getters
    public Transform Waypoint { get { return KeyWaypoint; } }
    public PianoKey Prev { get { return PrevKey; } }
    public PianoKey Next { get { return NextKey; } }
    public GameObject BouncingPad { get { return transform.GetChild(0).gameObject; } }

    // Use this for initialization
    void Start ()
    {
        m_Renderer = GetComponent<Renderer>();
        m_DefaultMaterial = m_Renderer.material;
	}

    // Called when this becomes the key selected by the player
    public void OnSelect()
    {
        m_Renderer.material = SelectedMaterial;
    }

    // Called when this stops being the key that is selected by the player
    public void OnDeselect()
    {
        m_Renderer.material = m_DefaultMaterial;
    }
}
