using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoSystem : MonoBehaviour
{
	public float limitTimer = 30;

	public static float limitTimerStc; 
	public static float timerStc; 
	public static float chronoStc;
	public static bool playing = false;

	bool gameIsStart = false; 
	float chrono = 0;

	private void Start()
	{
		playing = false;
		gameIsStart = false;
		limitTimerStc = limitTimer;
		chrono = 0; 
	}

	private void Update()
	{
		if (playing) chrono += Time.deltaTime;

		if (chrono >= limitTimer)
		{
            LevelManager.hasBeatTimer = false;
		}


		chronoStc = chrono;
		timerStc = limitTimer - chrono;
        timerStc = Mathf.Clamp(timerStc, 0, limitTimer);

		if (!gameIsStart)
		{
			playing = false; 
			if (Input.anyKeyDown) 
			{
				GameObject startText = GameObject.Find("StartText");
				if (startText != null) startText.SetActive(false); 
				playing = true;
				gameIsStart = true; 
			}
		}
	}
}
