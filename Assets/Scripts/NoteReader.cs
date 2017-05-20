using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteReader : MonoBehaviour {

    [SerializeField]
    // The number of seconds ahead the notes are displayed
    private float WindowSize = 5.0f;

    [SerializeField]
    private GameObject noteInstance;

    [SerializeField]
    private uint InitialTempo = 120;

    [SerializeField]
    private float TempoChangeRate = 1.0f;

    [SerializeField]
    private string notes = "abbabababa";

    [SerializeField]
    private int[] timings = { 1,2,3,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 };

    private int counter = 0;

    private List<NotesMovement> m_NotesQueue;

    private int m_AccumulatedTiming;

    // The time that has passed since the beginning of the song
    private float m_Time;

    private RectTransform staveUI;

    private float m_Tempo;

    // Getters
    public float SongTime { get { return m_Time; } }
    public float windowSize { get { return WindowSize; } }

    // Returns the equivalent of a specific number of beats in seconds, according to the current tempo
    public float BeatsToTime(int beats) { return (beats * 60.0f) / m_Tempo; }

	// Use this for initialization
	void Start () {
        staveUI = GameObject.FindGameObjectWithTag("Stave").GetComponent<RectTransform>();
        m_NotesQueue = new List<NotesMovement>();
        m_AccumulatedTiming = timings[0];
        m_Tempo = InitialTempo;
    }
	
	// Update is called once per frame
	void Update () {

        // Until pressing keys is implemented...
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnKeyPress(' ');
        }

        // Increment time
        if (m_NotesQueue.Count > 0)
            m_Time = Mathf.Min(m_Time + Time.deltaTime, BeatsToTime(m_NotesQueue[0].Timing));
        else
            m_Time += Time.deltaTime;

        // Spawn all notes inside the window
        if (BeatsToTime(m_AccumulatedTiming) < m_Time + WindowSize)
        {
            if (counter < notes.Length)
            {
                GameObject newNote = Instantiate(noteInstance, Vector3.zero, Quaternion.identity);
                newNote.transform.GetChild(0).gameObject.GetComponent<Text>().text = notes[counter].ToString();
                newNote.transform.SetParent(staveUI.transform, false);
                newNote.transform.localPosition += new Vector3(staveUI.rect.width, 0, 0);
                NotesMovement newNoteMovement = newNote.GetComponent<NotesMovement>();
                newNoteMovement.SetTiming(m_AccumulatedTiming);
                m_NotesQueue.Add(newNoteMovement);

                m_AccumulatedTiming += timings[counter + 1];

                counter++;
            }
        }
    }

    // Called when a key is pressed
    public void OnKeyPress(char key)
    {
        if (m_NotesQueue.Count > 0)
        {
            //(m_NotesQueue[0].Timing - m_Time);
            GameObject note = m_NotesQueue[0].gameObject;
            m_NotesQueue.RemoveAt(0);
            Destroy(note);
        }
    }
}
