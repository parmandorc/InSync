using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trollQuitButton : MonoBehaviour {

    public bool youSure = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void pressed()
    {
        Text text = GetComponent<Text>();
        if(youSure == true)
        {
            text.text = "Are you Sure?";
            youSure = false;
        }
        else if(youSure == false)
        {
            text.text = "Your leaving me.... ;( ";
            youSure = true;
        }
    }

}
