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
    public float delayClamTheme = 12f; 

    float valueState = 0;
    bool isRun = true;

    Vector3 oldPostionPlayer;
    float chrono = 0;
    int sceneIndex; 

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();

        valueState = 0;      
    }

    private void Update()
    {
        instance.setParameterByName("State", valueState);

        if (isRun && valueState > 0)
        {
            valueState -= transitionStartSpeed * Time.fixedDeltaTime; 
        }
        else if (!isRun && valueState < 1)
        {
            valueState += transitionEndSpeed * Time.fixedDeltaTime;
        }
        valueState = Mathf.Clamp(valueState, 0f, 1f); 
        DetectActionPlayer(); 
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    void DetectActionPlayer()
    {
        if (chrono >= delayClamTheme)
        {
            isRun = false;
        }

        if (Vector3.Distance(oldPostionPlayer, FPS_Controller.playerPos) <= 0.1f)
        {
            chrono += Time.fixedDeltaTime; 
        }
        else
        {
            isRun = true;
            chrono = 0; 
        }
        oldPostionPlayer = FPS_Controller.playerPos; 
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneIndex = scene.buildIndex;

        if (sceneIndex == 0)
        {
            Destroy(gameObject); 
        }
    }

}
