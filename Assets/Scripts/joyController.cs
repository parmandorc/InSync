using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joyController : MonoBehaviour {

    public Vector3 spawnLocation;
    public PlayerController player1;

    public PlayerController player2;

    public GameObject playerPrefab;

    public bool[] isUsed = new bool[11];

    Color[] colors = new Color[10];

    


    public PianoKey key;
	// Use this for initialization
	void Start () {
        spawnLocation = gameObject.transform.GetChild(0).position;

        colors[0] = Color.red;
        colors[1] = Color.yellow;
        colors[2] = Color.magenta;
        colors[3] = new Color(252, 88,0);
        colors[4] = Color.cyan;
        colors[5] = Color.blue;
        colors[6] = Color.green;
        colors[7] = new Color(247, 165, 24);
        colors[8] = new Color(196, 62, 178);
        colors[9] = new Color(122, 0, 252);


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
                    newPlayer.GetComponent<PlayerController>().setupCharacter("JoyA" + i, "Joy" + i,colors[(int)Random.Range(0,9)] , key);
                    break;
                }
                else
                {
                    print("i am working");
                }
            }

        }



    }

    
