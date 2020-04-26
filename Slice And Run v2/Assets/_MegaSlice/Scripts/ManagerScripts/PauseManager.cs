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

    public static bool gameIsStart = false; 

    private void Start()
    {
        gameIsStart = false; 
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0)
        {
            pauseScreen = GameObject.Find("Canvas").transform.Find("PauseScreen").gameObject;

            fps = GameObject.FindWithTag("Player").GetComponent<FPS_Controller>();
            playerAttack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        }
        playerInterface = GameObject.Find("PlayerInterface");

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
        Time.timeScale = 1;
        if (pauseScreen != null) pauseScreen.SetActive(false);
        if (playerInterface != null) playerInterface.SetActive(true);
        if(fps != null)
        {
            fps.canPlay = true;
            fps.canMoveCamera = true;
            playerAttack.canShot = true;
        }
        isPaused = false;

        Cursor.visible = false;
        if (FindObjectOfType<MouseControl>().collotRotation)
            Cursor.lockState = CursorLockMode.Confined;
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        ChronoSystem.playing = true;
        if (LevelManager.isLevelEnding)
        {
            playerInterface.SetActive(false);
            fps.StopPlayer(); 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            ChronoSystem.playing = false;
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        ChronoSystem.playing = false;
        if (playerInterface != null) playerInterface.SetActive(false);
        if (pauseScreen != null) pauseScreen.SetActive(true);
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if(fps != null)
        {
            fps.canPlay = false;
            fps.canMoveCamera = false;
            playerAttack.canShot = false;
        }
    }
}
