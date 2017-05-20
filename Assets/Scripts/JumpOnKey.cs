using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOnKey : MonoBehaviour {

    public GameObject currentWp;
    public GameObject currentKey; 


    public Rigidbody rb;
    public PlayerController pc;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        pc = GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        currentWp = pc.SelectedKey.Waypoint.gameObject;
        currentKey = pc.SelectedKey.BouncingPad;

        if (Input.GetButtonDown("Jump")){
            rb.MovePosition(currentKey.transform.position);
        }

        if (Input.GetButtonUp("Jump"))
        {
            rb.MovePosition(currentWp.transform.position);
        }
	}
}
