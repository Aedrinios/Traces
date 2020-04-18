using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MenuSettings : MonoBehaviour
{
    [Range(0,10)] public static float mouseSensitivity = 1f; 
    [Range(0,10)] public float globalVolume = 1f;
    [Range(0.1f, 10)] public float lumiosity = 1f;

    FMOD.Studio.Bus Global;
    PostProcessVolume volume;
    ColorGrading colorGradingLayer; 

    // Start is called before the first frame update
    void Awake()
    {
        Global = FMODUnity.RuntimeManager.GetBus("bus:/Global");
        volume = FindObjectOfType<PostProcessVolume>();
        volume.profile.TryGetSettings(out colorGradingLayer);

        colorGradingLayer.gamma.value = Color.black;
        colorGradingLayer.gamma.value.w = lumiosity - 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Global.setVolume(globalVolume);
        colorGradingLayer.gamma.value.w = lumiosity - 1f; 
    }
}
