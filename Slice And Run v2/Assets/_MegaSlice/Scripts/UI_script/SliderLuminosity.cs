using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderLuminosity : MonoBehaviour
{
    MenuSettings menuSettings; 

    // Start is called before the first frame update
    void Start()
    {
        menuSettings = FindObjectOfType<MenuSettings>(); 
    }

    // Update is called once per frame
    public void ChangeLuminosity(float newLumiosity)
    {
        menuSettings.luminosity = newLumiosity; 
    }
}
