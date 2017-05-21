using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NoteReader))]
public class GameMode : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject gameMenu;

    [SerializeField]
    private float WaitTimeAfterSongEnd;

    private NoteReader musicController;

    // Use this for initialization
    void Start ()
    {
        musicController = GetComponent<NoteReader>();
        NoteReader.OnSongEnd += OnSongEnd;
	}

    public void loadGame(string songTitle)
    {
        //sorting out UI 
        pauseMenu.SetActive(false);
        gameMenu.SetActive(true);

        musicController.Init(songTitle);
    }

    public void pauseGame()
    {
        //Sorting out ui 
        pauseMenu.SetActive(false);
        gameMenu.SetActive(true);
    }

    void OnSongEnd()
    {
        if (WaitTimeAfterSongEnd <= 0.0f)
        {
            Reset();
        }
        else
        {
            Invoke("Reset", WaitTimeAfterSongEnd);
        }
    }

    private void Reset()
    {
        pauseMenu.SetActive(true);
        gameMenu.SetActive(false);
    }
}
