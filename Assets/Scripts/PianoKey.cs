using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PianoKey : MonoBehaviour
{
    public delegate void GameEvent(string key);
    public static event GameEvent OnKeyHit;

    [SerializeField]
    private GameObject KeyMesh;

    [SerializeField]
    private Transform KeyWaypoint;

    [SerializeField]
    private PianoKey PrevKey, NextKey;

    [SerializeField]
    private string Key;

    // The renderer of the key mesh
    private Renderer m_Renderer;

    // The default color of the key when not selected
    private Color m_DefaultColor;

    // A stack of the players that have selected this key
    private List<PlayerController> m_PlayersSelected;

    private AudioSource m_KeySound;

    // Getters
    public Transform Waypoint { get { return KeyWaypoint; } }
    public PianoKey Prev { get { return PrevKey; } }
    public PianoKey Next { get { return NextKey; } }
    public GameObject BouncingPad { get { return transform.GetChild(0).gameObject; } }
    
    // Use this for initialization
    void Start ()
    {
        KeyMesh = gameObject.transform.GetChild(3).gameObject;
        m_Renderer = KeyMesh.GetComponent<Renderer>();
        m_DefaultColor = m_Renderer.material.color;
        m_PlayersSelected = new List<PlayerController>();
        m_KeySound = GetComponent<AudioSource>();
	}

    // Called when this becomes the key selected by the player
    public void OnSelect(PlayerController player)
    {
        m_PlayersSelected.Insert(0, player);
        m_Renderer.material.color = player.playerColor;
    }

    // Called when this stops being the key that is selected by the player
    public void OnDeselect(PlayerController player)
    {
        m_PlayersSelected.Remove(player);
        m_Renderer.material.color = (m_PlayersSelected.Count > 0) ? m_PlayersSelected[0].playerColor : m_DefaultColor;
    }

    // Called when this key is pressed
    public void OnHitKey()
    {
        m_KeySound.Play();

        if (OnKeyHit != null)
        {
            OnKeyHit(Key);
        }
    }
}
