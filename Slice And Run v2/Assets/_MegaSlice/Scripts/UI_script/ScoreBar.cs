using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ScoreBar : MonoBehaviour
{
	LifeTimerManager lifeTimerManager; 
	Image img;
	float ratioFill;
	Color originalColor; 

	private void Start()
	{
		lifeTimerManager = GameObject.FindObjectOfType<LifeTimerManager>();
		img = GetComponent<Image>();
		originalColor = img.color; 
	}

	private void Update()
	{
		ratioFill = LifeTimerManager.lifeTimer / lifeTimerManager.TimerMax;
		img.fillAmount = ratioFill;

        if(img.fillAmount < 0.2)
        {
            img.color = Color.Lerp(img.color, Color.red, 0.05f);
        }
        else
        {
            img.color = Color.Lerp(img.color, originalColor, 0.05f);
        }
	}
}
