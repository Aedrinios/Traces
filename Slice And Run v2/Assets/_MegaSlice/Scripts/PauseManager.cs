using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused;
    private GameObject pauseScreen;
    private GameObject playerInterface;
    private FPS_Controller fps;
    private PlayerAttack playerAttack;

    private void Start()
    {
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0)
            pauseScreen = GameObject.Find("Canvas").transform.Find("PauseScreen").gameObject;
        playerInterface = GameObject.Find("PlayerInterface");
        fps = GameObject.FindWithTag("Player").GetComponent<FPS_Controller>();
        playerAttack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();

        if (isPaused) isPaused = false; 
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
        pauseScreen.SetActive(false);
        playerInterface.SetActive(true);
        fps.canMoveCamera = true;
        playerAttack.canShot = true;
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (LevelManager.isLevelEnding)
        {
            //BasicTools.RestartScene(); 
            playerInterface.SetActive(false);
            fps.canMoveCamera = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    void Pause()
    {
        playerInterface.SetActive(false);
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        fps.canMoveCamera = false;
        playerAttack.canShot = false;


    }

}
