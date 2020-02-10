using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimerManager : MonoBehaviour
{
    [Header("Settings Life Timer")]
    public static float lifeTimer;

    private float timerMax;
    public float TimerMax { get { return timerMax;} private set { timerMax = value; } }
    public float timerStart = 70f;
    public static bool playing;

    private void OnEnable()
    {
        LevelManager.onLevelComplete += StopLifeTimer;
    }

    private void OnDisable()
    {
        LevelManager.onLevelComplete -= StopLifeTimer;
    }

    private void Start()
	{
		lifeTimer = timerStart;
        TimerMax = timerStart;
        playing = false;
	}

	private void Update()
	{
		if (playing)
		{
			lifeTimer -= Time.deltaTime;
			if (lifeTimer <= 0)
			{
				LevelManager.onLevelFailed?.Invoke(); 
			}
		}
        else
        {
            if (Input.anyKeyDown)
            {
                StartLifeTimer();
            }
        }
	}

	public void StartLifeTimer()
	{
		playing = true;
	}

    public void StopLifeTimer()
    {
        playing = false;
    }

    void ResetScene()
    {
        BasicTools.RestartScene();
    }
}
