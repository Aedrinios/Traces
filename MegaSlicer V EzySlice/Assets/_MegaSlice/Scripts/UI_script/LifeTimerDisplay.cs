using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class LifeTimerDisplay : MonoBehaviour
{
	Text myText;

	private void Start()
	{
		myText = GetComponent<Text>(); 
	}

	private void Update()
	{
		string displayText = Mathf.Floor(LifeTimerManager.lifeTimer).ToString();
		myText.text = displayText; 
		
	}
}
