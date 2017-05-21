using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tuneSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void selector(string newTune)
    {
        GameObject musicController = GameObject.FindGameObjectWithTag("MusicController");
        musicController.GetComponent<NoteReader>().ReadFile(newTune);
        print(newTune);
    }
}