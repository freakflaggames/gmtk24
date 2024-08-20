using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    PlayerDeath player;
    public GameObject baseObj;
    private void Awake()
    {
        player = FindAnyObjectByType<PlayerDeath>().gameObject.GetComponent<PlayerDeath>();
    }
    private void OnEnable()
    {
        PlayerDeath.playerDied += PlayerDied;
    }
    public void PlayerDied()
    {
        baseObj.SetActive(true);
    }
    public void RespawnPlayer()
    {
        player.Respawn();
        baseObj.SetActive(false);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void OnDisable()
    {
        PlayerDeath.playerDied -= PlayerDied;
    }
}
