using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesMovement : MonoBehaviour {

    public RectTransform trans;

    public float speed;


	// Use this for initialization
	void Start () {
        trans = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {

        trans.anchoredPosition = new Vector2(trans.anchoredPosition.x - speed, 0);
	}
}
