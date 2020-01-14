using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ScoreBar : MonoBehaviour
{
	LifeTimerManager fifeTimerManager; 
	Image img;
	float ratioFill;
	Color originalColor; 

	private void Start()
	{
		fifeTimerManager = GameObject.FindObjectOfType<LifeTimerManager>();
		img = GetComponent<Image>();
		originalColor = img.color; 
	}

	private void Update()
	{
		ratioFill = LifeTimerManager.lifeTimer / fifeTimerManager.timerMax;
		img.fillAmount = ratioFill;
	}
}
