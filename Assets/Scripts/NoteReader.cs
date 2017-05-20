using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteReader : MonoBehaviour {

    [SerializeField]
    // The number of beats ahead the notes are displayed
    private float WindowSize = 5.0f;

    [SerializeField]
    private GameObject noteInstance;

    [SerializeField]
    private uint InitialTempo = 120;

    [SerializeField]
    private uint MinimumTempo = 30;

    [SerializeField]
    // The rate at which the tempo decays while the track is stopped
    private float TempoDecayRate = 0.1f;

    [SerializeField]
    // The maximum beats the player can be delayed on pressing a key without the tempo decaying
    private float TempoDecayThreshold = 0.5f;

    [SerializeField]
    // The rate at which the tempo is increased when pressing a key too fast
    private float TempoIncreaseRate = 1.0f;

    [SerializeField]
    // The maximum beats the player can press a key in advance without the tempo being increased
    private float TempoIncreaseThreshold = 0.5f;

    private List<string> m_Notes;

    private List<int> m_Timings;
    
    private int counter = 0;

    private List<NotesMovement> m_NotesQueue;

    private int m_AccumulatedTiming;

    // The time that has passed since the beginning of the song
    private float m_Time;

    private RectTransform staveUI;

    private float m_Tempo;

    private float m_TimerSinceTimeStopped;

    // Getters
    public float SongTime { get { return m_Time; } }
    public float windowSize { get { return WindowSize; } }

    void Awake()
    {
        m_Notes = new List<string>();
        m_Timings = new List<int>();

        ReadFile();
    }

    // Use this for initialization
    void Start () {
        staveUI = GameObject.FindGameObjectWithTag("Stave").GetComponent<RectTransform>();
        m_NotesQueue = new List<NotesMovement>();
        m_AccumulatedTiming = Mathf.CeilToInt(WindowSize);
        m_Tempo = InitialTempo;
    }
	
	// Update is called once per frame
	void Update () {

        // Until pressing keys is implemented...
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnKeyPress(' ');
        }

        UpdateTempoDecay();

        // Increment time
        if (m_NotesQueue.Count > 0)
            m_Time = Mathf.Min(m_Time + Time.deltaTime * m_Tempo / 60.0f, m_NotesQueue[0].Timing);
        else
            m_Time += Time.deltaTime * m_Tempo / 60.0f;

        // Spawn all notes inside the window
        if (m_AccumulatedTiming < m_Time + WindowSize)
        {
            if (counter < m_Notes.Count)
            {
                GameObject newNote = Instantiate(noteInstance, Vector3.zero, Quaternion.identity);
                newNote.transform.GetChild(0).gameObject.GetComponent<Text>().text = m_Notes[counter].ToString();
                newNote.transform.SetParent(staveUI.transform, false);
                newNote.transform.localPosition += new Vector3(staveUI.rect.width, 0, 0);
                NotesMovement newNoteMovement = newNote.GetComponent<NotesMovement>();
                newNoteMovement.SetTiming(m_AccumulatedTiming);
                m_NotesQueue.Add(newNoteMovement);

                m_AccumulatedTiming += m_Timings[counter];

                counter++;
            }
        }
    }

    // Updates the management of tempo decay
    void UpdateTempoDecay()
    {
        if (m_NotesQueue.Count > 0)
        {
            if (Mathf.Approximately(m_Time, m_NotesQueue[0].Timing))
            {
                m_TimerSinceTimeStopped += Time.deltaTime;
                if (m_TimerSinceTimeStopped > TempoDecayThreshold)
                {
                    m_Tempo = Mathf.Max(m_Tempo - Time.deltaTime * TempoDecayRate, MinimumTempo);
                    print(m_Tempo);
                }
            }
            else
            {
                m_TimerSinceTimeStopped = 0.0f;
            }
        }
    }

    // Called when a key is pressed
    public void OnKeyPress(char key)
    {
        if (m_NotesQueue.Count > 0)
        {
            // Determine the increase in tempo
            float timeAdvance = m_NotesQueue[0].Timing - m_Time;
            if (timeAdvance >= TempoIncreaseThreshold)
            {
                m_Tempo += timeAdvance * TempoIncreaseRate;
                print(m_Tempo);
            }

            GameObject note = m_NotesQueue[0].gameObject;
            m_NotesQueue.RemoveAt(0);
            Destroy(note);
        }
    }

    void ReadFile()
    {
        string path = System.IO.Path.Combine(Application.dataPath, "SongsData");
        path = System.IO.Path.Combine(path, "test.csv");

        string[] lines = System.IO.File.ReadAllLines(path);
        foreach (string line in lines)
        {
            ProcessLine(line);
        }
    }

    void ProcessLine(string line)
    {
        string notes = "";
        string[] fields = line.Split(new char[] {','});
        
        if (fields.Length > 1)
        {
            int timing;
            if (int.TryParse(fields[0].Trim(), out timing))
            {
                m_Timings.Add(timing);

                for (int i = 1; i < fields.Length; i++)
                {
                    notes += fields[i].Trim().ToUpper();
                }

                m_Notes.Add(notes);
            }
        }
    }
}
