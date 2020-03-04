using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoSystem : MonoBehaviour
{
	public float limitTimer = 30;

	public static float timerStc; 
	public static float chronoStc;
	public static bool playing = false;

	bool gameIsStart = false; 
	float chrono = 0;

	private void Start()
	{
		playing = false;
		gameIsStart = false;
		chrono = 0; 
	}

	private void Update()
	{
		if (playing) chrono += Time.deltaTime;

		if (chrono >= limitTimer)
		{
			LevelManager.onLevelFailed?.Invoke();
		}

		chrono = Mathf.Clamp(chrono, 0, limitTimer); 
		chronoStc = chrono;
		timerStc = limitTimer - chrono; 

		if (!gameIsStart)
		{
			playing = false; 
			if (Input.anyKeyDown) 
			{
				GameObject.Find("StartText").SetActive(false); 
				playing = true;
				gameIsStart = true; 
			}
		}
	}
}
