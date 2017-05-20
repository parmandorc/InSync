using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteReader : MonoBehaviour {

    public float uiOfset = 50; 

    public GameObject staveUI;

    public GameObject noteInstance;

    [Range(0.3f,5)]
    public float tempo = 0.3f;


    public string notes = "abbabababa";
    public float[] timings = { 1,2,3,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 };


    public int counter = 0;

    public float timer;

    GameObject newNote;


	// Use this for initialization
	void Start () {
        timer = 1;
	}
	
	// Update is called once per frame
	void Update () {

    

        if (timer <= 0)
        {
            if(counter < notes.Length)
            {

                
                newNote = Instantiate(noteInstance, new Vector3(transform.position.x , transform.position.y, transform.position.z), Quaternion.identity);

                //newNote.GetComponent<Text>().text = notes[counter].ToString();

                newNote.transform.GetChild(0).gameObject.GetComponent <Text>().text = notes[counter].ToString();    //= notes[counter].ToString();
                newNote.transform.SetParent(staveUI.transform,false);
                newNote.transform.localPosition += new Vector3(uiOfset, 0, 0);

                counter++;
                //timer = 1;
                print(timings[counter]);
                timer = timings[counter]/2;
                
            }
        }




        timer -= tempo / 50;             // old working without timings 30;
        	
	}

    // Changing tempo functions 
    void increaseTempo(float value){ tempo += value; }
    void decreaseTempo(float value){ tempo -= value; }
}
