using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused;
    private GameObject pauseScreen;
    private GameObject playerInterface;

    private void Start()
    {
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0)
            pauseScreen = GameObject.Find("Canvas").transform.Find("PauseScreen").gameObject;
        playerInterface = GameObject.Find("PlayerInterface");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Resume()
    {
        playerInterface.SetActive(true);
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        playerInterface.SetActive(false);
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
