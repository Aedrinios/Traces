using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimerManager : MonoBehaviour
{
	[Header("Settings Life Timer")]
	public static float lifeTimer;
	public float timerStart = 70f; 
	public float timerMax = 100;
	public float multiplierSpeedTimer = 1;
	public float multiplierBonusCut = 1; 
	[HideInInspector] public bool playing = false;

	public static float multiplierBonusCutStatic; 

	private void Start()
	{
		playing = false;
		lifeTimer = timerStart;
		multiplierBonusCutStatic = multiplierBonusCut;
		timerMax += 0.95f; 
	}

	private void Update()
	{
		if (playing)
		{
			lifeTimer -= Time.deltaTime * multiplierSpeedTimer;
			lifeTimer = Mathf.Clamp(lifeTimer, 0, timerMax);

			if (lifeTimer <= 0)
			{
				Debug.Log("TU AS PERDU");
				Invoke("ResetScene", 1f); 
			}
		}
	}

	private void FixedUpdate()
	{
		lifeTimer = Mathf.Clamp(lifeTimer, 0, timerMax);
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
