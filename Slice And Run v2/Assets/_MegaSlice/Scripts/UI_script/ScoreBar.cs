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

	private void Start()
	{
		chronoSystem = GameObject.FindObjectOfType<ChronoSystem>();


		img = GetComponent<Image>();
		originalColor = img.color; 
	}

	private void Update()
	{
		ratioFill = 1 - (ChronoSystem.chronoStc / chronoSystem.limitTimer);
		img.fillAmount = ratioFill;
	}

	void ColorChange()
	{
		if (img.fillAmount < 0.2)
		{
			img.color = Color.Lerp(img.color, Color.red, 0.05f);
		}
		else
		{
			img.color = Color.Lerp(img.color, originalColor, 0.05f);
		}
	}
}
