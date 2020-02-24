using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FmodParameter : MonoBehaviour
{
    FMOD.Studio.EventInstance instance;
    [FMODUnity.EventRef]
    public string fmodEvent;
    public string nameParameter;
    [Range(0, 1f)] public float valueParameter;
    public int delay = 0;  

    private void OnEnable()
    {
        PlaySound(); 
    }

    private void Update()
    {
        if (nameParameter != null)
        {
            instance.setParameterByName(nameParameter, valueParameter);
        }
    }

    private void OnDisable()
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); 
    }

    public void PlaySound()
    {
        instance = RuntimeManager.CreateInstance(fmodEvent);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(Camera.main.transform.position));
        
        instance.setTimelinePosition(delay);
        instance.start();
    }
}
