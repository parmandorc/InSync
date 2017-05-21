using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joyController : MonoBehaviour {

    [SerializeField]
    private GameObject SpawnLocation;

    [SerializeField]
    private PianoKey DefaultKey;

    [SerializeField]
    private GameObject playerPrefab;

    private bool[] isUsed = new bool[11];

    private bool isKeyboardUsed1, isKeyboardUsed2;

    // Update is called once per frame
    void Update() {

        if (!isKeyboardUsed1 && Input.GetButtonDown("Jump1"))
        {
            isKeyboardUsed1 = true;
            SpawnPlayer("Jump1", "Horizontal1", Color.blue, DefaultKey);
        }

        if (!isKeyboardUsed2 && Input.GetButtonDown("Jump2"))
        {
            isKeyboardUsed2 = true;
            SpawnPlayer("Jump2", "Horizontal2", Color.blue, DefaultKey);
        }

        for (int i = 1; i < 12; i++)
        {
            if (Input.GetButtonUp("JoyA" + i) && isUsed[i - 1] == false)  
            {
                isUsed[i - 1] = true;
                SpawnPlayer("JoyA" + i, "Joy" + i, Color.blue, DefaultKey);
                break;
            }
        }
    }

    void SpawnPlayer(string enterButton, string moveAxis, Color colorKey, PianoKey key)
    {
        GameObject newPlayer = Instantiate(playerPrefab, SpawnLocation.transform.position, Quaternion.identity);
        newPlayer.GetComponent<PlayerController>().setupCharacter(enterButton, moveAxis, colorKey, key);
    }
}