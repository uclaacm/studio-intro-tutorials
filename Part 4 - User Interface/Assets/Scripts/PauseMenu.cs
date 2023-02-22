using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; //useful for later if you want to change audio and such
    public GameObject pauseMenuUI;

    void Update() //for keybinds to pause the game
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); //gets rid of the pause menu panel
        Time.timeScale = 1f; //resumes time
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); //turns on the pause menu panel
        Time.timeScale = 0f; //stops time
        GameIsPaused = true; 
    }
}
