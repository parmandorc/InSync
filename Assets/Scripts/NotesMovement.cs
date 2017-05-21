using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesMovement : MonoBehaviour
{
    [SerializeField]
    private Text NoteElementUI;

    private RectTransform trans;
    private float speed;
    private NoteReader musicController;
    private int m_Timing;
    private RectTransform m_StaveUI;

    private Dictionary<string, Text> m_NoteElements;

    public int Timing { get { return m_Timing; } }

    // Use this for initialization
    void Start ()
    {
        trans = GetComponent<RectTransform>();
        musicController = GameObject.FindGameObjectWithTag("MusicController").GetComponent<NoteReader>();
        m_StaveUI = transform.parent.GetComponent<RectTransform>();
	}
	
    public void Init(List<string> notes, int timing)
    {
        m_NoteElements = new Dictionary<string, Text>();

        foreach (string note in notes)
        {
            Text noteText = Instantiate<Text>(NoteElementUI);
            noteText.transform.SetParent(transform);
            noteText.text = note;
            m_NoteElements.Add(note, noteText);
        }

        m_Timing = timing;
    }

    public void OnKeyPressed(string key)
    {
        m_NoteElements[key].text = "";
    }

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
