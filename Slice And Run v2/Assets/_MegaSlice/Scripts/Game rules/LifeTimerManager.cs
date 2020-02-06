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
    [SerializeField] private  bool playing;

	private void Start()
	{
		lifeTimer = timerStart;
        TimerMax = timerStart;
	}

	private void Update()
	{
		if (playing)
		{
			lifeTimer -= Time.deltaTime;
			if (lifeTimer <= 0)
			{
				Invoke("ResetScene", 1f); 
			}
		}
	}

	void StartLifeTimer()
	{
		playing = true;
	}

	void ResetScene()
	{
		BasicTools.RestartScene(); 
	}

	private void OnEnable()
	{
		EventHandler.BeginTimer += StartLifeTimer; 
	}

	private void OnDisplay()
	{
		EventHandler.BeginTimer += StartLifeTimer;
	}
}
