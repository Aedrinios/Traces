using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutGainScore : MonoBehaviour
{
	//temporaire : pour le moment toutes les découpes donnent le même score
	public int bonusCut = 10; 

	void GainScore()
	{
		ScoreManager.GainScore(bonusCut); 
	}

	private void OnEnable()
	{
		EventHandler.cutObject += GainScore;
	}

	private void OnDisable()
	{
		EventHandler.cutObject -= GainScore;
	}


}
