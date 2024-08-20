using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField] public float timerCounter;
    [SerializeField] private int minutes;
    [SerializeField] private int seconds;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Update()
    {
        timerCounter += Time.deltaTime;
        minutes = Mathf.FloorToInt(timerCounter / 60f);
        seconds = Mathf.FloorToInt(timerCounter - minutes * 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
