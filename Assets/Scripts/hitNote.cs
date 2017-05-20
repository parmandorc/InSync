using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class hitNote : MonoBehaviour {

    public AudioSource audio; 
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            audio.Play();
            print("hit");
        }


        //Destroy(other.gameObject);
    }
}