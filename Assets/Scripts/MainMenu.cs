using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Main menu buttons parent; settings parent; credits parent; main menu parent.
    public GameObject menuObj, settingsObj, creditsObj;

    //Start button
    public void StartGame()
    {
        ToMainMenu();
    }

    //Settings button
    public void Settings()
    {
        menuObj.SetActive(false);
        settingsObj.SetActive(true);
    }

    //Credits button
    public void Credits()
    {
        menuObj.SetActive(false);
        creditsObj.SetActive(true);
    }

    //Quit game button -- ENABLE for win/mac build, DISABLE for webGL
    public void QuitGame()
    {
        Application.Quit();
    }

    //Returns to main menu
    public void ToMainMenu()
    {
        settingsObj.SetActive(false);
        creditsObj.SetActive(false);
        menuObj.SetActive(true);
    }
}
