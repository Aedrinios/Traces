using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class MenuSettings : MonoBehaviour
{
    [Range(0,10)] public float globalVolume = 1f;
    [Range(0.1f, 10)] public float luminosity = 1f;
    [Range(0, 10)] public float mouseSensitivity = 1f;

    FMOD.Studio.Bus Global;
    PostProcessVolume volume;
    ColorGrading colorGradingLayer;
    FPS_Controller fps; 

    // Start is called before the first frame update
    void Awake()
    {
        InitialiseSettings();
    }

    // Update is called once per frame
    void Update()
    {
        Global.setVolume(globalVolume);
        colorGradingLayer.gamma.value.w = luminosity - 1f;
        if (fps != null) fps.multiplierSensitivity = mouseSensitivity; 
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; 
;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitialiseSettings(); 
    }

    void InitialiseSettings()
    {
        Global = FMODUnity.RuntimeManager.GetBus("bus:/Global");
        volume = FindObjectOfType<PostProcessVolume>();
        volume.profile.TryGetSettings(out colorGradingLayer);
        fps = FindObjectOfType<FPS_Controller>();
    }
}
