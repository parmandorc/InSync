using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesMovement : MonoBehaviour {

    private RectTransform trans;
    private float speed;
    private GameObject musicController;

    // Use this for initialization
    void Start () {
        trans = GetComponent<RectTransform>();

        musicController = GameObject.FindGameObjectWithTag("MusicController");
	}
	
	// Update is called once per frame
	void Update () {

        speed = musicController.GetComponent<NoteReader>().tempo;
        trans.anchoredPosition = new Vector2(trans.anchoredPosition.x - speed, 0);
	}
}
