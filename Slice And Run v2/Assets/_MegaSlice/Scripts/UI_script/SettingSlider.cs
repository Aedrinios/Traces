using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SettingSlider : MonoBehaviour
{
    public int id = 1; 
    MenuSettings menuSettings;    

    void Start()
    {
        menuSettings = FindObjectOfType<MenuSettings>();
        SetValueSlider(); 
    }

    void SetValueSlider()
    {
        Slider sliderScript = GetComponent<Slider>();  

        if (id == 1)
        {
            sliderScript.value = menuSettings.globalVolume; 
        }
        else if (id == 2)
        {
            sliderScript.value = menuSettings.luminosity;
        }
        else if (id == 3)
        {
            sliderScript.value = menuSettings.mouseSensitivity;
        }
    }

    public void ChangeVolume(float newVolume)
    {
        menuSettings.globalVolume = newVolume;
    }

    public void ChangeLuminosity(float newLuminosity)
    {
        menuSettings.luminosity = newLuminosity;
    }

    public void ChangeSensibility(float newSensibility)
    {
        menuSettings.mouseSensitivity = newSensibility;
    }
}
