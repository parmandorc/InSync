using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour {

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject gameMenu;

    private GameObject musicController;


    // Use this for initialization
    void Start () {
        musicController = gameObject.transform.GetChild(0).gameObject;
	}

    public void loadGame(string songTitle)
    {
        musicController.GetComponent<NoteReader>().ReadFile(songTitle);

        //sorting out UI 
        pauseMenu.SetActive(false);
        gameMenu.SetActive(true);
    }

    public void pauseGame()
    {

    
        //Sorting out ui 
        pauseMenu.SetActive(false);
        gameMenu.SetActive(true);

    }
}
