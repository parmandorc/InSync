using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteReader : MonoBehaviour
{
    public delegate void GameEvent();
    public static event GameEvent OnSongEnd;

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

    private List<List<string>> m_Notes;

    private List<int> m_Timings;
    
    private int counter;

    private List<NotesMovement> m_NoteObjectsQueue;

    private List<List<string>> m_NotesQueue;

    private int m_AccumulatedTiming;

    // The time that has passed since the beginning of the song
    private float m_Time;

    [SerializeField]
    private RectTransform staveUI;

    private float m_Tempo;

    private float m_TimerSinceTimeStopped;

    private bool m_IsPaused = true;

    // Getters
    public float SongTime { get { return m_Time; } }
    public float windowSize { get { return WindowSize; } }

    void Awake()
    {
        PianoKey.OnKeyHit += OnKeyPress;

        // The raw data song
        m_Notes = new List<List<string>>();
        m_Timings = new List<int>();

        // The current state of the song
        m_NoteObjectsQueue = new List<NotesMovement>();
        m_NotesQueue = new List<List<string>>();
    }
	
    // Resets the game state to start a new song
    public void Init(string songFile)
    {
        // Reset raw data song
        m_Notes.Clear();
        m_Timings.Clear();

        // Reset current state of the song
        m_NoteObjectsQueue.Clear();
        m_NotesQueue.Clear();

        // Read song data
        ReadFile(songFile);

        // Reset gameplay data
        m_Time = 0.0f;
        m_AccumulatedTiming = Mathf.CeilToInt(WindowSize);
        m_Tempo = InitialTempo;
        counter = 0;
        m_IsPaused = false;
    }

	// Update is called once per frame
	void Update ()
    {
        if (!m_IsPaused)
        {
            UpdateTempoDecay();

            // Increment time
            if (m_NoteObjectsQueue.Count > 0)
                m_Time = Mathf.Min(m_Time + Time.deltaTime * m_Tempo / 60.0f, m_NoteObjectsQueue[0].Timing);
            else
                m_Time += Time.deltaTime * m_Tempo / 60.0f;

            // Spawn all notes inside the window
            if (m_AccumulatedTiming < m_Time + WindowSize)
            {
                if (counter < m_Notes.Count)
                {
                    GameObject newNote = Instantiate(noteInstance, Vector3.zero, Quaternion.identity);
                    newNote.transform.SetParent(staveUI.transform, false);
                    newNote.transform.localPosition += new Vector3(staveUI.rect.width, 0, 0);
                    NotesMovement newNoteMovement = newNote.GetComponent<NotesMovement>();
                    newNoteMovement.Init(m_Notes[counter], m_AccumulatedTiming);
                    m_NoteObjectsQueue.Add(newNoteMovement);
                    m_NotesQueue.Add(m_Notes[counter]);

                    m_AccumulatedTiming += m_Timings[counter];

                    counter++;
                }
            }
        }
    }

    // Updates the management of tempo decay
    private void UpdateTempoDecay()
    {
        if (m_NoteObjectsQueue.Count > 0)
        {
            if (Mathf.Approximately(m_Time, m_NoteObjectsQueue[0].Timing))
            {
                m_TimerSinceTimeStopped += Time.deltaTime;
                if (m_TimerSinceTimeStopped > TempoDecayThreshold)
                {
                    m_Tempo = Mathf.Max(m_Tempo - Time.deltaTime * TempoDecayRate, MinimumTempo);
                }
            }
            else
            {
                m_TimerSinceTimeStopped = 0.0f;
            }
        }
    }

    // Called when a key is pressed
    public void OnKeyPress(string key)
    {
        if (!m_IsPaused && m_NotesQueue.Count > 0 && m_NotesQueue[0].Contains(key))
        {
            // Determine the increase in tempo
            float timeAdvance = m_NoteObjectsQueue[0].Timing - m_Time;
            if (timeAdvance >= TempoIncreaseThreshold)
            {
                m_Tempo += timeAdvance * TempoIncreaseRate;
            }

            // Remove the pressed key from the top of the queue
            m_NoteObjectsQueue[0].OnKeyPressed(key);
            m_NotesQueue[0].Remove(key);
            if (m_NotesQueue[0].Count == 0)
            {
                // If all keys in the top of the queue were pressed, continue
                GameObject note = m_NoteObjectsQueue[0].gameObject;
                m_NoteObjectsQueue.RemoveAt(0);
                m_NotesQueue.RemoveAt(0);

                Destroy(note);

                // Check song end
                if (m_NotesQueue.Count <= 0 && counter >= m_Notes.Count)
                {
                    m_IsPaused = true;

                    if (OnSongEnd != null)
                    {
                        OnSongEnd();
                    }
                }
            }
        }
    }

    private void ReadFile(string file)
    {
        string path = System.IO.Path.Combine(Application.dataPath, "SongsData");
        path = System.IO.Path.Combine(path, file);

        string[] lines = System.IO.File.ReadAllLines(path);
        foreach (string line in lines)
        {
            ProcessLine(line);
        }
    }

    private void ProcessLine(string line)
    {
        List<string> notes = new List<string>();
        string[] fields = line.Split(new char[] {','});
        
        if (fields.Length > 1)
        {
            int timing;
            if (int.TryParse(fields[0].Trim(), out timing))
            {
                m_Timings.Add(timing);

                for (int i = 1; i < fields.Length; i++)
                {
                    notes.Add(fields[i].Trim());
                }

                m_Notes.Add(notes);
            }
        }
    }
}
