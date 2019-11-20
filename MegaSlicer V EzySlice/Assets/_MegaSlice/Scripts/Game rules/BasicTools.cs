using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class BasicTools : MonoBehaviour
{
    public static void NextScene()
    {
        int lengthScenes = SceneManager.sceneCountInBuildSettings;
        int numberNextScene = SceneManager.GetActiveScene().buildIndex + 1;
        
        if (numberNextScene < lengthScenes)
        {
            SceneManager.LoadScene(numberNextScene);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public static void RestartScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
}
