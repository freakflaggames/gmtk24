using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseBaseObj, pauseMenuObj, settingsObj;
    bool paused;

    void Start()
    {
        pauseBaseObj.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
            PauseGame();
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
            Resume();
    }

    void PauseGame()
    {
        pauseBaseObj.SetActive(true);
        //actually pause the game
        paused = true;
    }

    public void Resume()
    {
        BackToPause();
        pauseBaseObj.SetActive(false);
        //actually unpause
        paused = false;
    }

    public void LastCheckpoint()
    {
        //add functionality
    }

    public void Settings()
    {
        settingsObj.SetActive(true);
        pauseMenuObj.SetActive(false);
    }

    public void BackToPause()
    {
        settingsObj.SetActive(false);
        pauseMenuObj.SetActive(true);
    }

    public void ToMainMenu()
    {
        //To main menu scene
    }
}
