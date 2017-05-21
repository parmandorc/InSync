using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joyController : MonoBehaviour {

    public Vector3 spawnLocation;
    public PlayerController player1;

    public PlayerController player2;

    public GameObject playerPrefab;

    public bool[] isUsed = new bool[11];



    public PianoKey key;
	// Use this for initialization
	void Start () {
        spawnLocation = gameObject.transform.GetChild(0).position;


	}

    // Update is called once per frame
    void Update() {

        
        for (int i = 1; i < 12; i++)
        {
            if (Input.GetButtonUp("JoyA" + i) && isUsed[i - 1] == false)  
            {
                    isUsed[i-1] = true;
                    print("JoyA" + i + "  its this one dummy" + "   Joy " + i);
                    GameObject newPlayer = Instantiate(playerPrefab, spawnLocation, Quaternion.identity);
                    newPlayer.GetComponent<PlayerController>().setupCharacter("JoyA" + i, "Joy" + i, Color.blue, key);
                    break;
                }
                else
                {
                    print("i am working");
                }
            }

        }



    }

    
