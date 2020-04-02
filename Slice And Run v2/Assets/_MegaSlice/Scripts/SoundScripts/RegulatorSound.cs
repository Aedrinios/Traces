using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegulatorSound : MonoBehaviour
{
    public float decreaseVolume = 0.1f; 
    AudioSource audioSrc;
    RegulatorSound[] otherRegulator; 

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        otherRegulator = FindObjectsOfType<RegulatorSound>(); 
        if (otherRegulator.Length > 1)
        {
            //audioSrc.volume -= decreaseVolume * (otherRegulator.Length - 1); 
            
        }
    }
}
