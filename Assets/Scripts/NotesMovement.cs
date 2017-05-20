using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesMovement : MonoBehaviour {

    private RectTransform trans;
    private float speed;
    private NoteReader musicController;
    private float m_Timing;
    private RectTransform m_StaveUI;

    public float Timing { get { return m_Timing; } }

    // Use this for initialization
    void Start () {
        trans = GetComponent<RectTransform>();
        musicController = GameObject.FindGameObjectWithTag("MusicController").GetComponent<NoteReader>();
        m_StaveUI = transform.parent.GetComponent<RectTransform>();
	}
	
    public void SetTiming(float timing) { m_Timing = timing; }

	// Update is called once per frame
	void Update () {

        trans.anchoredPosition = new Vector2((m_Timing - musicController.SongTime) * m_StaveUI.rect.width / musicController.windowSize, 0);

        // Destroy when out of the screen
        if (trans.position.x < 0)
        {
            Destroy(gameObject);
        }
	}
}
