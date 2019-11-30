using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimerManager : MonoBehaviour
{
	public static float lifeTimer;
	public float timerStart = 70f; 
	public float timerMax = 100; 

	[HideInInspector] public bool playing = false;

	private void Start()
	{
		playing = false;
		lifeTimer = timerStart; 
	}

	private void Update()
	{
		if (playing)
		{
			lifeTimer -= Time.deltaTime;
			lifeTimer = Mathf.Clamp(lifeTimer, 0, timerMax); 

			if (lifeTimer <= 0)
			{
				Debug.Log("TU AS PERDU");
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
		EventHandler.BeginGame += StartLifeTimer; 
	}

	private void OnDisplay()
	{
		EventHandler.BeginGame += StartLifeTimer;
	}
}
