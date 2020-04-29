using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMusicParameter : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    [FMODUnity.EventRef]
    public string fmodEvent;
    public float transitionStartSpeed = 0.1f;
    public float transitionEndSpeed = 0.1f; 
    float valueState = 0;

    bool isRun = true;

    bool playerStart = false; 

    private void Update()
    {
        instance.setParameterByName("State", valueState);

        if (isRun && valueState > 0)
        {
            valueState -= transitionStartSpeed * Time.deltaTime; 
        }
        else if (!isRun && valueState < 1)
        {
            valueState += transitionEndSpeed * Time.deltaTime;
        }

        if (!playerStart)
        {
            if (Input.anyKeyDown)
            {
                playerStart = true;
                isRun = true; 
            }
        }
    }

    void OnEnable()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();

        SceneManager.sceneLoaded += OnSceneLoaded;

        LevelManager.onLevelComplete += BoardParameter;
        LevelManager.onLevelFailed += BoardParameter;        
    }

    private void OnDisable()
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        LevelManager.onLevelComplete -= BoardParameter;
        LevelManager.onLevelFailed -= BoardParameter;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        playerStart = false; 
    }

    void RunParameter()
    {
        isRun = true;
    }

    void BoardParameter()
    {
        isRun = false;
    }


}
