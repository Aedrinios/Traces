using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class EventText : MonoBehaviour
{
	public float delayDisplay = 2f; 
	Text myText;

	string wallJumpText = "Wall Jump Unlock !!!";
	string pastequeRainText = "Watermelon Rain !!!";

	private void Start()
	{
		myText = GetComponent<Text>();
		myText.text = ""; 
	}

	void DisplayWallJump()
	{
		StartCoroutine(DisplayText(wallJumpText)); 
	}

	void DisplayPastequeRain()
	{
		StartCoroutine(DisplayText(pastequeRainText));
	}

	IEnumerator DisplayText(string newText)
	{
		myText.text = newText;
		yield return new WaitForSeconds(delayDisplay);
		myText.text = "";
	}

	private void OnEnable()
	{
		EventHandler.GainWallJump += DisplayWallJump;
		EventHandler.StartPastequeRain += DisplayPastequeRain;
	}

	private void OnDisable()
	{
		EventHandler.GainWallJump -= DisplayWallJump;
		EventHandler.StartPastequeRain -= DisplayPastequeRain;
	}


}
