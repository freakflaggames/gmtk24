using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    public TimerController time;
    [SerializeField] public float timeVal;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        timeVal = time.timerCounter;
        
        if (collision.gameObject.CompareTag("Player"))
        {
           SceneManager.LoadScene("WinScreen");
        }
    }
}
