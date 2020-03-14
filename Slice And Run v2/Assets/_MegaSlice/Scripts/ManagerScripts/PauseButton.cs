using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    GameManager gameManager;
    PauseManager pauseManager; 

    void Start()
    {
        GameObject gameManagerGo = GameObject.FindWithTag("GameManager");
        gameManager = gameManagerGo.GetComponent<GameManager>();
        pauseManager = gameManagerGo.GetComponent<PauseManager>(); 
    }

    public void Resume()
    {
        pauseManager.Resume(); 
    }

    public void Pause()
    {
        pauseManager.Pause();
    }

    public void GoToMenu()
    {
        gameManager.LaunchLevel(0);
        Time.timeScale = 1;
    }

    public void RetryLevel()
    {
        gameManager.RetryLevel(); 
    }

    public void LaunchNextLevel()
    {
        gameManager.LaunchNextLevel(); 
    }

}
