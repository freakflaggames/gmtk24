using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{ 
    //game objects for referencing
    public GameObject screenshake, volume, textspeed;
    //actual components
    Toggle screenShakeToggle;
    Slider volumeSlider, textSpeedSlider;

    void Start()
    {
        screenShakeToggle = screenshake.GetComponent<Toggle>();
        volumeSlider = volume.GetComponent<Slider>();
        textSpeedSlider = textspeed.GetComponent<Slider>();
    }

    public void ScreenShake()
    {
        //get the toggle value: screenShakeToggle.isOn
    }

    public void Volume()
    {
        //get the slider value: volumeSlider.value
    }   

    public void TextSpeed()
    {
        //get the slider value: textSpeedSlider.value
    }
}
