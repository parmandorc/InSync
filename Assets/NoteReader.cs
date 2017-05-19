using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteReader : MonoBehaviour {

    public GameObject staveUI;

    public AudioSource A;
    public AudioSource B;
    public AudioSource C;
    public AudioSource D;
    public AudioSource E;



    public GameObject noteInstance; 

    public float tempo = 0;
    public string notes = "abbabababa";

    public int counter = 0;

    public float timer = 1;

    GameObject newNote;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    

        if (timer <= 0)
        {
            if(counter < notes.Length)
            {
                newNote = Instantiate(noteInstance, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

                //newNote.GetComponent<Text>().text = notes[counter].ToString();

                newNote.transform.GetChild(0).gameObject.GetComponent <Text>().text = notes[counter].ToString();    //= notes[counter].ToString();
                newNote.transform.parent = staveUI.transform;
                
                if(notes[counter].ToString() == "A")
                {
                    A.Play();
                } 


                counter++;
                timer = 1;
            }
        }


       

        timer -= 0.01f;
        	
	}
}
