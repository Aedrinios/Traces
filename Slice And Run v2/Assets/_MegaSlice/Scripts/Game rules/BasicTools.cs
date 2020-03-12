using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class BasicTools : MonoBehaviour
{
    [HideInInspector] public static bool isInvicible;

    public delegate void EventHappen();
    public static EventHappen resetEvent;

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
        resetEvent.Invoke(); 
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        isInvicible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isInvicible = !isInvicible;
        }

    }


}
