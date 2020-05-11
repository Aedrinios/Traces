using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDoorManager : MonoBehaviour
{
    public float delayBeforeStart = 1f; 
    public float delay = 0.25f;
    public bool trigger = false;

    private void Update()
    {
        if (trigger)
        {
            Invoke("StartOpenDoor", delayBeforeStart); 
            trigger = false; 
        }
    }

    public void StartOpenDoor()
    {
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        bool playing = true;
        float chrono = 0;

        WipeoutSystem[] wipeoutScript = GetComponentsInChildren<WipeoutSystem>(); 
        int i = 0; 

        while (playing)
        {
            chrono += Time.deltaTime; 
            if (chrono >= delay)
            {
                wipeoutScript[i].trigger = true;
                i++; 
                chrono = 0; 
            }
            if (i >= wipeoutScript.Length)
            {
                playing = false; 
            }
            yield return null; 
        }
    }

}
