using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [Header("Time control")]
    [SerializeField] private float slowPower;
    [SerializeField] private float smooth;
    [SerializeField] private float slowDuration;

    private float originalTimeScale;
    private float timePast;
    public bool slowed;

    void Start()
    {
        originalTimeScale = Time.timeScale;
        timePast = 0f;
    }

    void Update()
    {
        float showTimeScale = Time.timeScale;
        if (slowed)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, slowPower, Time.unscaledDeltaTime * smooth); ;
            timePast += Time.unscaledDeltaTime;
            ChronoSystem.playing = false;
            if (timePast > slowDuration)
            {
                slowed = false;
                timePast = 0f;
                Time.timeScale = originalTimeScale;
                LevelManager.onLevelComplete?.Invoke();
            }
        }
        else
        {
            Time.timeScale = originalTimeScale;
        }
    }



}
