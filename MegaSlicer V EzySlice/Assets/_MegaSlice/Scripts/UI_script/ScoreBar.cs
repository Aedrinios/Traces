using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ScoreBar : MonoBehaviour
{
	
	 Color colorWarning = Color.yellow;
	public Color colorDanger = Color.red;

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

		if (ratioFill <= 0.1f)
		{
			img.color = colorDanger;
		}
		else if (ratioFill <= 0.33f)
		{
			img.color = colorWarning;
		}
		else
		{
			img.color = originalColor;
		}



	}
}
