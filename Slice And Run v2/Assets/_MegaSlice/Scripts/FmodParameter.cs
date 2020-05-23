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
    public float valueParameter; 

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
        StopSound(); 
    }

    public void PlaySound()
    {
        instance = RuntimeManager.CreateInstance(fmodEvent);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));       

        instance.start();
    }

    public void StopSound()
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
