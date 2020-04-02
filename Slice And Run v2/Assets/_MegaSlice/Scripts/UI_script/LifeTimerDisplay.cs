using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class LifeTimerDisplay : MonoBehaviour
{
	TextMeshProUGUI timeText;
    public float minTimer;
    public float maxTimer;
    private Color originalColor;

    private void Start()
	{
        timeText = GetComponent<TextMeshProUGUI>();
        originalColor = timeText.color;
	}

	private void Update()
	{
        float currentTime = Mathf.Clamp(LifeTimerManager.lifeTimer, minTimer, maxTimer);
        if(currentTime < 20f)
        {
            timeText.color = Color.Lerp(timeText.color, Color.red, 0.05f);
        }
        else
        {
            timeText.color = Color.Lerp(timeText.color, originalColor, 0.05f);
        }
        timeText.text = currentTime.ToString("F2"); 
	}
}


