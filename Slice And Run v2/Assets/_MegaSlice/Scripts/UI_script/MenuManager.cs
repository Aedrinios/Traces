using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public KeyCode key = KeyCode.Escape;

    public GameObject pauseButton;
    public GameObject settingScreen; 

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            pauseButton.SetActive(true);
            settingScreen.SetActive(false);
        }
    }

}
