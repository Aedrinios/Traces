using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FmodTrigger : MonoBehaviour
{
    FMOD.Studio.EventInstance instance;
    [FMODUnity.EventRef]
    public string fmodEvent;

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
