using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitNote : MonoBehaviour
{
    private PianoKey key; 

	// Use this for initialization
	void Start ()
    {
        key = transform.parent.gameObject.GetComponent<PianoKey>();
    }
	
    void OnTriggerEnter(Collider other)
    {
        key.OnHitKey();
    }
}