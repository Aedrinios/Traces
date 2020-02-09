using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [Header("Time control")]
    [SerializeField] private float slowPower;
    //  [SerializeField] private float smooth;
    [SerializeField] private float slowDuration;

    private float originalTimeScale;
    private float timePast;
     public bool slowed;


    // Start is called before the first frame update
    void Start()
    {
        originalTimeScale = Time.timeScale;
        timePast = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float showTimeScale = Time.timeScale;
        if (slowed)
        {
            Time.timeScale = slowPower;
            timePast += Time.unscaledDeltaTime;
            LifeTimerManager.playing = false;
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
            //  Time.timeScale = Mathf.Lerp(Time.timeScale, originalTimeScale, Time.unscaledDeltaTime * smooth);
            Time.timeScale = originalTimeScale;
        }
    }
}
