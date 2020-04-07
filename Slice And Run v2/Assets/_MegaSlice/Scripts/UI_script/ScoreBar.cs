using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ScoreBar : MonoBehaviour
{
	ChronoSystem chronoSystem; 
	Image img;
	float ratioFill;
	Color originalColor;

	float lerpRatio = 0.005f; 

	private void Start()
	{
		chronoSystem = GameObject.FindObjectOfType<ChronoSystem>();
		img = GetComponent<Image>();
		originalColor = img.color; 
	}

	private void Update()
	{
		ratioFill = (ChronoSystem.timerStc / chronoSystem.limitTimer);
		img.fillAmount = ratioFill;
		ColorChange(); 
	}

	void ColorChange()
	{
		if (img.fillAmount < 0.25)
		{
			img.color = Color.Lerp(img.color, Color.red, lerpRatio);
		}
		else
		{
			img.color = Color.Lerp(img.color, originalColor, lerpRatio);
		}
	}
}
