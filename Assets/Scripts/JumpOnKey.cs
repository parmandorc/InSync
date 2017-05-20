using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOnKey : MonoBehaviour {


    public float speed = 1.0f;
    private float startTime;
    private float journeyLength;

    [Range(1,30)]
    public float ang_scl = 1;

    public GameObject currentWp;
    public GameObject currentKey;

    [SerializeField]
    private bool isJumping = false;
    private bool isJumpDown = true;

    private enum jumpStates{ notJumping, forwardJumping, backwardJumping };
    jumpStates currentJumpState = jumpStates.notJumping; 

    public Rigidbody rb;
    public PlayerController pc;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        pc = GetComponent<PlayerController>();

       //currentWp = pc.SelectedKey.Waypoint.gameObject;
       //currentKey = pc.SelectedKey.BouncingPad;

        startTime = Time.time;
        //journeyLength = Vector3.Distance(currentWp.transform.position, currentKey.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        currentWp = pc.SelectedKey.Waypoint.gameObject;
        currentKey = pc.SelectedKey.BouncingPad;

        if(currentJumpState == jumpStates.notJumping)
        {
            if (Input.GetButtonDown("Jump"+ pc.playerID))
            {
                currentJumpState = jumpStates.forwardJumping;
                startTime = Time.time;
                journeyLength = Vector3.Distance(currentWp.transform.position, currentKey.transform.position);
            }
        }

        if(currentJumpState == jumpStates.forwardJumping)
        {
            float distCovered = (Time.time - startTime) * speed;
            rb.MovePosition(hermit(currentWp.transform.position, currentKey.transform.position, new Vector3(0, ang_scl, 0), new Vector3(0, ang_scl, 0), distCovered));
            if (distCovered >= 1)
            {
                //rb.MovePosition(currentWp.transform.position);
                currentJumpState = jumpStates.backwardJumping;
                startTime = Time.time;
            }
        }

        if(currentJumpState == jumpStates.backwardJumping)
        {
            float distCovered = (Time.time - startTime) * speed;
            rb.MovePosition(hermit(currentKey.transform.position, currentWp.transform.position, new Vector3(0, ang_scl, 0), new Vector3(0, ang_scl, 0), distCovered));
            if (distCovered >= 1)
            {
                //rb.MovePosition(currentWp.transform.position);
                currentJumpState = jumpStates.notJumping;
            }
        }

        
        //rb.MovePosition(Vector3.Lerp(startMarker.position, endMarker.position, fracJourney));
    }

    static public Vector3 hermit(Vector3 p0, Vector3 p1, Vector3 t0, Vector3 t1, float t)
    {
        float[] coeff = new float[4];
        coeff[0] = 1 - (3 * t * t) + (2 * t * t * t);
        coeff[1] = t * t * (3 - 2 * t);
        coeff[2] = t * (1 - 2 * t + t * t);
        coeff[3] = t * t * (1 - t);

        // More Efficient then for loop
        Vector3 result = Vector3.zero;
        result += coeff[0] * p0;
        result += coeff[1] * p1;
        result += coeff[2] * t0;
        result += coeff[3] * t1;

        return result;
    }
}
