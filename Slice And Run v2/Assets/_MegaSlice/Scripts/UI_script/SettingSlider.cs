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

    public void ResetValue()
    {
        if (id == 1)
        {
            menuSettings.globalVolume = 1;
        }
        else if (id == 2)
        {
            menuSettings.luminosity = 1;
        }
        else if (id == 3)
        {
            menuSettings.mouseSensitivity = 1;
        }
        SetValueSlider(); 
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
